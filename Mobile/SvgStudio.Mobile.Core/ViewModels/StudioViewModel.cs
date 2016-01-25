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
        private string _currentStepName = null;
        private List<DesignRegionViewModel> _designRegionViewModels = new List<DesignRegionViewModel>();

        public StudioViewModel(string templateId, ContentView stepView, IStorageRepository db)
        {
            _templateId = templateId;
            _stepView = stepView;
            _db = db;
            Steps = new ObservableCollection<IStudioStep>();
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
            CurrentStepName = Steps.First().DisplayText;
            _previewMarkup = GeneratePreviewMarkup();
        }

        public Command NextStepCommand { get; set; }
        public Command PreviousStepCommand { get; set; }
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

        public string CurrentStepName
        {
            get
            {
                return _currentStepName;
            }
            set
            {
                if (_currentStepName != value)
                {
                    _currentStepName = value;
                    FirePropertyChanged();
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
    }
}   
