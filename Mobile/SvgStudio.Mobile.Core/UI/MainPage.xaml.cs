using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Mobile.Core.Models.Storage;
using SvgStudio.Shared.ServiceContracts.Responses;
using System.Reflection;
using System.ComponentModel;
using SVG.Forms.Plugin.Abstractions;
using SvgStudio.Mobile.Core.Services;

namespace SvgStudio.Mobile.Core.UI
{
    public partial class MainPage : ContentPage
    {
        public class ViewModel : INotifyPropertyChanged
        {
            private string _svgImagePath = null;

            public string SvgImagePath
            {
                get
                {
                    return _svgImagePath;
                }
                set
                {
                    if (value != _svgImagePath)
                    {
                        _svgImagePath = value;
                        if (PropertyChanged != null)
                        {
                            PropertyChanged(this, new PropertyChangedEventArgs("SvgImagePath"));
                        }
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        ViewModel vm = null;

        public MainPage()
        {
            InitializeComponent();
            MyButton.Clicked += async (sender, args) =>
            {
                await Button_Clicked(sender, args);
            };
        }

        Random random = new Random();
         
        private async Task Button_Clicked(object sender, EventArgs args)
        {
            var button = (Button)sender;
            button.Text = "Sync...";

            var sync = new DatabaseSynchronizer(DependencyService.Get<IDatabaseConnectionProvider>(), new MobileServiceGateway("http://192.168.1.14:14501/"));
            var response = await sync.SynchronizeModelWithServer();

            if (response != null)
            {
                SummaryLabel.Text = string.Join(Environment.NewLine, response.Select(x => x.ToString()));
                button.Text = "Done";
            }
            else
            {
                button.Text = "Error";
            }

            //< abstractions:SvgImage x:Name = "img" SvgPath = "{Binding SvgImagePath}" HeightRequest = "500" WidthRequest = "350" BackgroundColor = "White" HorizontalOptions = "Center" VerticalOptions = "Center" />
        }
    }
}
