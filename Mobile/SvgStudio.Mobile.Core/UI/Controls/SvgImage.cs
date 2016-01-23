using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio.Mobile.Core.UI.Controls
{
    public class SvgImage : Xamarin.Forms.Image, INotifyPropertyChanged
    {
        public static readonly BindableProperty SvgMarkupProperty = 
            BindableProperty.Create("SvgMarkup", typeof(string), typeof(SvgImage), default(string));

        public string SvgMarkup
        {
            get { return (string)GetValue(SvgMarkupProperty); }
            set { SetValue(SvgMarkupProperty, value); }
        }
    }
}
