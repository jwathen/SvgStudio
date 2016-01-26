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
        private List<DesignRegionViewModel> _designRegionViewModels = new List<DesignRegionViewModel>();
        private bool _changingStep = false;

        public StudioViewModel(string templateId, ContentView stepView, IStorageRepository db)
        {
            _templateId = templateId;
            _stepView = stepView;
            _db = db;
            Steps = new ObservableCollection<IStudioStep>();
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
                var designRegionViewModel = new DesignRegionViewModel(designRegion, null, _db);
                _designRegionViewModels.Add(designRegionViewModel);
                designRegionViewModel.Init();
                designRegionViewModel.PropertyChanged += DesignRegionViewModel_PropertyChanged;
                designRegionViewModel.Steps.CollectionChanged += DesignRegionViewModel_Steps_CollectionChanged;
                foreach (var step in designRegionViewModel.Steps)
                {
                    Steps.Add(step);
                }
            }
            CurrentStep = Steps.First();
            _previewMarkup = GeneratePreviewMarkup();
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
        public ObservableCollection<IStudioStep> Steps { get; set; }

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

        private void DesignRegionViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private void DesignRegionViewModel_Steps_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Steps.Clear();
            foreach(var step in _designRegionViewModels.SelectMany(x => x.Steps))
            {
                Steps.Add(step);
            }
        }

        private string GeneratePreviewMarkup()
        {
            var renderer = new TemplateRenderer(_template);
            foreach (var designRegionViewModel in _designRegionViewModels)
            {
                renderer.AddDesign(designRegionViewModel.DesignRegion.Name, designRegionViewModel.Design.Shape, designRegionViewModel.Design.Palette);
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

        }
    }
}   
