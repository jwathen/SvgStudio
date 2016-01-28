using SvgStudio.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.UI.Controls
{
    public partial class PaletteOptionControl : ContentView
    {
        public PaletteOptionControl(PaletteOptionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
