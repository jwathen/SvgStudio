using SvgStudio.Mobile.Core.UI.Controls;
using SvgStudio.Shared;
using SvgStudio.Shared.Drawing;
using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.Materializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public class StudioViewModel : ViewModelBase
    {
        private readonly string _templateId;
        private readonly ContentView _stepView;
        private readonly IStorageRepository _db;
        private Template _template;

        private string _previewMarkup = null;
        private IStudioStep _currentStep = null;
        private bool _changingStep = false;

        public StudioViewModel(string templateId, ContentView stepView, IStorageRepository db)
        {
            _templateId = templateId;
            _stepView = stepView;
            _db = db;
            DesignRegionSteps = new List<PickDesignStep>();
            Steps = new List<IStudioStep>();
            NextStepCommand = new Command(NextStep);
            PreviousStepCommand = new Command(PreviousStep);
            StepSwipedCommand = new Command<MR.Gestures.SwipeEventArgs>(StepSwiped);
            ShowStepPickerCommand = new Command(ShowStepPicker);
        }

        public void Init()
        {
            var drawingFactory = new DrawingFactory(_db);
            _template = drawingFactory.BuildTemplate(_templateId);
            foreach (var designRegion in _template.DesignRegions)
            {
                var pickDesignStep = new PickDesignStep(null, null, designRegion, _db);
                pickDesignStep.DecendentStepsChanged += DesignRegionStep_DecendentStepsChanged;
                pickDesignStep.PropertyChanged += DesignRegionStep_PropertyChanged;
                DesignRegionSteps.Add(pickDesignStep);
                Steps.Add(pickDesignStep);
            }
            CurrentStep = Steps.First();
            _previewMarkup = GeneratePreviewMarkup();
        }

        private void DesignRegionStep_DecendentStepsChanged(object sender, EventArgs e)
        {
            Steps.Clear();
            Steps.AddRange(DesignRegionSteps.SelectMany(x => x.GetDescendentsAndSelf()));
        }

        public void ShowStep()
        {
            if (_changingStep)
            {
                return;
            }
            _changingStep = true;
            CurrentStep.Start(_stepView, () => _changingStep = false);
        }

        public Command NextStepCommand { get; set; }
        public Command PreviousStepCommand { get; set; }
        public Command StepSwipedCommand { get; set; }
        public Command ChangeStepCommand { get; set; }
        public Command ShowStepPickerCommand { get; set; }
        public List<PickDesignStep> DesignRegionSteps { get; set; }
        public List<IStudioStep> Steps { get; set; }

        public string PreviewMarkup
        {
            get
            {
                return _previewMarkup;
            }
            set
            {
                if (_previewMarkup != value)
                {
                    _previewMarkup = value;
                    FirePropertyChanged();
                }
            }
        }

        public IStudioStep CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                if (_currentStep != value)
                {
                    _currentStep = value;
                    FirePropertyChanged();
                    ShowStep();
                }
            }
        }

        private void DesignRegionStep_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Design")
            {
                Task.Run(() => GeneratePreviewMarkup())
                    .ContinueWith(task =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            PreviewMarkup = task.Result;
                        });
                    });
            }
        }

        private string GeneratePreviewMarkup()
        {
            var renderer = new TemplateRenderer(_template);
            foreach (var designStep in DesignRegionSteps)
            {
                renderer.AddDesign(designStep.DesignRegion.Name, designStep.Design.Shape, designStep.Design.Palette);
            }
            XDocument svg = new XDocument(renderer.Render("Master"));
            string result = XmlHelper.RenderDocument(svg, true);
            return result;
        }

        private void NextStep()
        {
            if (_changingStep)
            {
                return;
            }
            int index = Steps.IndexOf(CurrentStep);
            index++;
            if (index < Steps.Count)
            {
                CurrentStep = Steps.ElementAt(index);
            }
        }

        private void PreviousStep()
        {
            if (_changingStep)
            {
                return;
            }
            int index = Steps.IndexOf(CurrentStep);
            index--;
            if (index >= 0)
            {
                CurrentStep = Steps.ElementAt(index);
            }
        }
        private void StepSwiped(MR.Gestures.SwipeEventArgs e)
        {
            if (e.Direction == MR.Gestures.Direction.Left)
            {
                NextStep();
            }
            else if (e.Direction == MR.Gestures.Direction.Right)
            {
                PreviousStep();
            }
        }

        private void ShowStepPicker()
        {
            Application.Current.MainPage.DisplayAlert("Steps", string.Join(Environment.NewLine, Steps.Select(x => x.DisplayText)), "Ok");
        }
    }
}   
