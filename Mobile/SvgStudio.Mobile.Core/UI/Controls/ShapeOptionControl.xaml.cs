using SvgStudio.Mobile.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.UI.Controls
{
    public partial class ShapeOptionControl : ContentView
    {
        public ShapeOptionControl(ShapeOptionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Image.GestureRecognizers.Add(new TapGestureRecognizer { Command = viewModel.SelectCommand });
        }
    }
}
