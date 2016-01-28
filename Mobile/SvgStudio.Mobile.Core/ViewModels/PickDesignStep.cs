using SvgStudio.Mobile.Core.UI.Controls;
using SvgStudio.Shared.Drawing;
using SvgStudio.Shared.Materializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public class PickDesignStep : StudioStepBase
    {
        private readonly IStorageRepository _db;
        private Shape _selectedShape = null;
        private Palette _selectedPalette = null;
        private Design _design = null;

        public PickDesignStep(Shape selectedShape, Palette selectedPalette, DesignRegion designRegion, IStorageRepository db)
        {
            _db = db;
            DesignRegion = designRegion;
            _selectedShape = selectedShape;
            _selectedPalette = selectedPalette;
            RecalculateDesign();
            DisplayText = string.Format("{0} Shape", designRegion.Name);
            this.PropertyChanged += This_PropertyChanged;
        }

        public DesignRegion DesignRegion { get; protected set; }
        public Shape SelectedShape
        {
            get
            {
                return _selectedShape;
            }
            set
            {
                if (_selectedShape != value)
                {
                    _selectedShape = value;
                    FirePropertyChanged();
                }
            }
        }
        public Palette SelectedPalette
        {
            get
            {
                return _selectedPalette;
            }
            set
            {
                if (_selectedPalette != value)
                {
                    _selectedPalette = value;
                    FirePropertyChanged();
                }
            }
        }
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

        public ShapeGalleryViewModel ShapeGallery { get; private set; }

        public override void Start(ContentView content, Action callback)
        {
            Task.Run(() =>
            {
                var compatibilityTags = _db.LoadCompatibilityTagsByDesignRegionId(DesignRegion.StorageId);
                var storageShapes = _db.LoadShapesByCompatibilityTagIds(compatibilityTags.Select(x => x.Id).ToList());
                var factory = new DrawingFactory(_db);
                var shapes = storageShapes.Select(x => factory.BuildShape(x)).ToList();
                var shapeGallery = new ShapeGalleryViewModel(shapes, 70, 70, _db);
                ShapeGallery = shapeGallery;
                shapeGallery.Init();
                shapeGallery.PropertyChanged += ShapeGallery_PropertyChanged;
                return shapeGallery;
            }).ContinueWith(galleryTask =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    content.Content = new ShapeGalleryControl(galleryTask.Result);
                    callback();
                });
            });
        }

        private void ShapeGallery_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedOption")
            {
                SelectedShape = ShapeGallery.SelectedOption.Shape;
            }
        }

        private void PickPaletteStep_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPalette")
            {
                var pickPaletteStep = (PickPaletteStep)sender;
                SelectedPalette = pickPaletteStep.SelectedPalette;
            }
        }

        private void This_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedShape")
            {
                foreach(var oldPickPaletteStep in ChildSteps.OfType<PickPaletteStep>().ToList())
                {
                    oldPickPaletteStep.PropertyChanged -= PickPaletteStep_PropertyChanged;
                    ChildSteps.Remove(oldPickPaletteStep);
                }

                // TODO add child steps for template shape
                if (SelectedShape.CanBeRecolored())
                {
                    var pickPaletteStep = new PickPaletteStep(SelectedPalette, SelectedShape, DesignRegion, _db);
                    pickPaletteStep.PropertyChanged += PickPaletteStep_PropertyChanged;
                    ChildSteps.Add(pickPaletteStep);
                }
                RecalculateDesign();
            }
            if (e.PropertyName == "SelectedPalette")
            {
                RecalculateDesign();
            }
        }

        private void RecalculateDesign()
        {
            Design = new Design
            {
                Shape = SelectedShape,
                Palette = SelectedPalette
            };
        }
    }
}
