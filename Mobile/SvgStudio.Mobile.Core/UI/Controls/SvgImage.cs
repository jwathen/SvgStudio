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
        private static Func<string> noop = () => (string)null;

        public static readonly BindableProperty SvgMarkupProperty = 
            BindableProperty.Create("SvgMarkup", typeof(string), typeof(SvgImage), default(string));

        public static readonly BindableProperty SvgMarkupAccessorProperty =
            BindableProperty.Create("SvgMarkupAccessor", typeof(Func<string>), typeof(SvgImage), noop);

        public string SvgMarkup
        {
            get { return (string)GetValue(SvgMarkupProperty); }
            set { SetValue(SvgMarkupProperty, value); }
        }

        public Func<string> SvgMarkupAccessor
        {
            get { return (Func<string>)GetValue(SvgMarkupAccessorProperty); }
            set { SetValue(SvgMarkupAccessorProperty, value); }
        }
    }
}
