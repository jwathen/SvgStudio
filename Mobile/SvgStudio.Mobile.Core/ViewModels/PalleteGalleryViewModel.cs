using SvgStudio.Shared.Drawing;
using SvgStudio.Shared.Materializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public class PaletteGalleryViewModel : ViewModelBase
    {
        private readonly IStorageRepository _db;
        private readonly List<Palette> _palettes;
        private readonly Shape _shape;
        private readonly double _optionWidth;
        private readonly double _optionHeight;
        private PaletteOptionViewModel _selectedOption = null;

        public PaletteGalleryViewModel(List<Palette> palettes, Shape shape, double optionWidth, double optionHeight, IStorageRepository db)
        {
            _db = db;
            _palettes = palettes;
            _shape = shape;
            _optionWidth = optionWidth;
            _optionHeight = optionHeight;
            Options = new ObservableCollection<PaletteOptionViewModel>();
        }

        public ObservableCollection<PaletteOptionViewModel> Options { get; private set; }

        public void Init()
        {
            var options = _palettes.Select(x => new PaletteOptionViewModel(x, _shape, _optionWidth, _optionHeight, _db)).ToList();
            foreach (var option in options)
            {
                option.PropertyChanged += Option_PropertyChanged;
                Options.Add(option);
            }
        }

        private void Option_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                var changedOption = (PaletteOptionViewModel)sender;
                if (changedOption.IsSelected)
                {
                    foreach (var option in Options)
                    {
                        if (option != sender)
                        {
                            option.IsSelected = false;
                        }
                    }
                    SelectedOption = changedOption;
                }
            }
        }

        public PaletteOptionViewModel SelectedOption
        {
            get
            {
                return _selectedOption;
            }
            set
            {
                if (_selectedOption != value)
                {
                    _selectedOption = value;
                    FirePropertyChanged();
                }
            }
        }
    }
}
