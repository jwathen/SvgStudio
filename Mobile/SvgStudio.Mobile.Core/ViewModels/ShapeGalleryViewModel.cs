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
    public class ShapeGalleryViewModel : ViewModelBase
    {
        private readonly IStorageRepository _db;
        private readonly List<Shape> _shapes = null;
        private readonly double _optionWidth;
        private readonly double _optionHeight;
        private ShapeOptionViewModel _selectedOption = null;

        public ShapeGalleryViewModel(List<Shape> shapes, double optionWidth, double optionHeight, IStorageRepository db)
        {
            _db = db;
            _shapes = shapes;
            _optionWidth = optionWidth;
            _optionHeight = optionHeight;
            Options = new ObservableCollection<ShapeOptionViewModel>();
        }

        public ObservableCollection<ShapeOptionViewModel> Options { get; private set; }

        public void Init()
        {
            var options = _shapes.Select(x => new ShapeOptionViewModel(x, _optionWidth, _optionHeight, _db)).ToList();
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
                var changedOption = (ShapeOptionViewModel)sender;
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

        public ShapeOptionViewModel SelectedOption
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
