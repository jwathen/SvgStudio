using SvgStudio.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.UI.Controls
{
    public partial class PaletteGalleryControl : ContentView
    {
        public PaletteGalleryControl(PaletteGalleryViewModel viewModel)
        {
            InitializeComponent();
            foreach(var option in viewModel.Options)
            {
                GalleryWrap.Children.Add(new PaletteOptionControl(option));
            }
        }
    }
}
