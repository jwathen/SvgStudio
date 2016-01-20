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
using System.IO;

namespace SvgStudio.Mobile.Core.UI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MyButton.Clicked += Button_Clicked;
        }

        Random random = new Random();

        private void Button_Clicked(object sender, EventArgs args)
        {
            var button = (Button)sender;
            button.Text = "Sync...";
            //string endpoint = "http://192.168.1.14:14501/";
            string endpoint = "http://172.16.17.166:14501/";
            var sync = new DatabaseSynchronizer(DependencyService.Get<IDatabaseConnectionProvider>(), new MobileServiceGateway(endpoint));
            Task.Run(sync.SynchronizeModelWithServer)
                .ContinueWith(result =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (result != null)
                        {
                            SummaryLabel.Text = string.Join(Environment.NewLine, result.Result.Select(x => x.ToString()));
                            button.Text = "Done";
                        }
                        else
                        {
                            button.Text = "Error";
                        }
                    });
                });

            //SvgImage image = new SvgImage();
            //image.HeightRequest = 600;
            //image.WidthRequest = 600;
            //image.SvgAssembly = typeof(MainPage).GetTypeInfo().Assembly;
            //image.SvgPath = "SvgStudio.Mobile.Core.drawing.svg";
            //image.BackgroundColor = Color.White;
            //image.HorizontalOptions = LayoutOptions.Center;
            //Images.Children.Add(image);

            //         var html = new HtmlWebViewSource();

            //html.Html = "<h1>Hi! " + i.ToString() + "</h1>";
            //i++;


            //web.HeightRequest = 1600;
            //web.WidthRequest = 500;

            //         if (i <= 3)
            //         {
            //             web.Source = html;
            //         }
            //         else
            //         {
            //	html.Html = "<html><body>" + (await GetDrawingAsync()) + "</body></html>";
            //	web.Source = html;
            //         }

            //< abstractions:SvgImage x:Name = "img" SvgPath = "{Binding SvgImagePath}" HeightRequest = "500" WidthRequest = "350" BackgroundColor = "White" HorizontalOptions = "Center" VerticalOptions = "Center" />
        }

        int i = 0;

        private async Task<string> GetDrawingAsync()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            using (var resourceStream = assembly.GetManifestResourceStream("SvgStudio.Mobile.Core.drawing.svg"))
            using (var reader = new StreamReader(resourceStream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}