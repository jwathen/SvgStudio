using SvgStudio.Shared.Drawing;
using SvgStudio.Shared.Materializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public class DesignRegionViewModel : ViewModelBase
    {
        private readonly IStorageRepository _db;
        private Design _design = null;

        public DesignRegionViewModel(DesignRegion designRegion, Design currentDesign, IStorageRepository db)
        {
            _db = db;
            DesignRegion = designRegion;
            Design = currentDesign;
            Steps = new ObservableCollection<IStudioStep>();
        }

        public void Init()
        {
            if (Design != null)
            {
                PickShapeStep = new PickShapeStep(Design.Shape, DesignRegion, _db);
                PickPaletteStep = new PickPaletteStep();
            }
            else
            {
                PickShapeStep = new PickShapeStep(null, DesignRegion, _db);
                PickPaletteStep = new PickPaletteStep();
            }

            PickShapeStep.PropertyChanged += PickShapeStep_PropertyChanged;
            PickShapeStep.ChildSteps.CollectionChanged += ShapeOrPaletteChildSteps_CollectionChanged;
            PickPaletteStep.PropertyChanged += PickPaletteStep_PropertyChanged;
            PickPaletteStep.ChildSteps.CollectionChanged += ShapeOrPaletteChildSteps_CollectionChanged;

            RecalculateDesign();
        }

        public DesignRegion DesignRegion { get; private set; }
        public PickShapeStep PickShapeStep { get; set; }
        public PickPaletteStep PickPaletteStep { get; set; }
        public Design Design
        {
            get
            {
                return _design;
            }
            set
            {
                if (_design != value)
                {
                    _design = value;
                    FirePropertyChanged();
                }
            }
        }
        public ObservableCollection<IStudioStep> Steps { get; private set; }

        private void PickShapeStep_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedShape")
            {
                RecalculateDesign();
            }
        }

        private void PickPaletteStep_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Palette")
            {
                RecalculateDesign();
            }
        }

        private void ShapeOrPaletteChildSteps_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RecalculateSteps();
        }

        private void RecalculateDesign()
        {
            var design = new Design();
            design.Shape = PickShapeStep.SelectedShape;
            if (design.Shape != null && design.Shape.CanBeRecolored() && PickPaletteStep != null)
            {
                design.Palette = PickPaletteStep.Palette;
            }
            Design = design;
            RecalculateSteps();
        }

        private void RecalculateSteps()
        {
            List<IStudioStep> newSteps = new List<IStudioStep>();
            newSteps.Add(PickShapeStep);
            newSteps.AddRange(PickShapeStep.ChildSteps);
            if (PickShapeStep.SelectedShape != null && PickShapeStep.SelectedShape.CanBeRecolored())
            {
                newSteps.Add(PickPaletteStep);
                newSteps.AddRange(PickPaletteStep.ChildSteps);
            }
            Steps.Clear();
            foreach(var step in newSteps)
            {
                Steps.Add(step);
            }
        }
    }
}
