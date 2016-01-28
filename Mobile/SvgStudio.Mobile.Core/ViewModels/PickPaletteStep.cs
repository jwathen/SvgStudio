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
    public class PickPaletteStep : StudioStepBase
    {
        private readonly IStorageRepository _db;
        private readonly DesignRegion _designRegion;
        private Palette _selectedPalette = null;
        private Shape _shape = null;

        public PickPaletteStep(Palette selectedPalette,  Shape shape, DesignRegion designRegion, IStorageRepository db)
        {
            _db = db;
            _designRegion = designRegion;
            _selectedPalette = selectedPalette;
            _shape = shape;
            DisplayText = string.Format("{0} Coloring", designRegion.Name);
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
        public PaletteGalleryViewModel Gallery { get; private set; }

        public override void Start(ContentView content, Action callback)
        {
            Task.Run(() =>
            {
                var storagePalettes = _db.LoadPalettes();
                var factory = new DrawingFactory(_db);
                var palettes = new List<Palette>();
                palettes.Add(null);
                palettes.AddRange(storagePalettes.Select(x => factory.BuildPalette(x.Id)).ToList());
                var gallery = new PaletteGalleryViewModel(palettes, _shape, 70, 70, _db);
                Gallery = gallery;
                gallery.Init();
                gallery.PropertyChanged += Gallery_PropertyChanged;
                return gallery;
            }).ContinueWith(galleryTask =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    content.Content = new PaletteGalleryControl(galleryTask.Result);
                    callback();
                });
            });
        }

        private void Gallery_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedOption")
            {
                SelectedPalette = Gallery.SelectedOption.Palette;
            }
        }
    }
}
