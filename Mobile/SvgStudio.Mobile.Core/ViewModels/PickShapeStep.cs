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
    public class PickShapeStep : ViewModelBase, IStudioStep
    {
        private readonly IStorageRepository _db;
        private readonly DesignRegion _designRegion;
        private Shape _selectedShape = null;

        public PickShapeStep(Shape selectedShape, DesignRegion designRegion, IStorageRepository db)
        {
            _db = db;
            _designRegion = designRegion;
            _selectedShape = selectedShape;
            DisplayText = string.Format("{0} Shape", designRegion.Name);
            ChildSteps = new ObservableCollection<IStudioStep>();
        }

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
                    // TODO add child steps if changd to templated shape
                }
            }
        }
        public ShapeGalleryViewModel Gallery { get; private set; }
        public string DisplayText { get; private set; }
        public ObservableCollection<IStudioStep> ChildSteps { get; private set; }

        public Task Start(ContentView content)
        {
            return Task.Run(() =>
            {
                var compatibilityTags = _db.LoadCompatibilityTagsByDesignRegionId(_designRegion.StorageId);
                var storageShapes = _db.LoadShapesByCompatibilityTagIds(compatibilityTags.Select(x => x.Id).ToList());
                var factory = new DrawingFactory(_db);
                var shapes = storageShapes.Select(x => factory.BuildShape(x)).ToList();
                var gallery = new ShapeGalleryViewModel(shapes, 70, 70);
                Gallery = gallery;
                gallery.Init();
                gallery.PropertyChanged += Gallery_PropertyChanged;
                return gallery;
            }).ContinueWith(galleryTask =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    content.Content = new ShapeGalleryControl(galleryTask.Result);
                });
            });
        }

        private void Gallery_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedOption")
            {
                SelectedShape = Gallery.SelectedOption.Shape;
            }
        }
    }
}
