using SvgStudio.Shared.Drawing;
using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.Materializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.ViewModels
{
    public class PaletteOptionViewModel : ViewModelBase
    {
        private readonly IStorageRepository _db;
        private readonly double _width;
        private readonly double _height;
        private bool _isSelected;

        public PaletteOptionViewModel(Palette palette, Shape shape, double width, double height, IStorageRepository db)
        {
            Palette = palette;
            Shape = shape;
            _db = db;
            _width = width;
            _height = height;
            SelectCommand = new Command(() => IsSelected = true);
            PreviewMarkupAccessor = RenderPreviewMarkup;
        }

        public Palette Palette { get; private set; }
        public Shape Shape { get; private set; }
        public Func<string> PreviewMarkupAccessor { get; private set; }
        public Command SelectCommand { get; set; }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    FirePropertyChanged();
                }
            }
        }

        private string RenderPreviewMarkup()
        {
            var renderDesignResult = Shape.Render(Palette, "Preview");
            var svg = renderDesignResult.AsStandaloneSvg(_width, _height);
            return XmlHelper.RenderDocument(svg, true);
        }
    }
}
