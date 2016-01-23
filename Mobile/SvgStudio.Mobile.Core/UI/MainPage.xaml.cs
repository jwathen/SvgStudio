using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Mobile.Core.Models.Storage;
using System.Reflection;
using System.ComponentModel;

using SvgStudio.Mobile.Core.Services;
using System.IO;
using SvgStudio.Mobile.Core.UI.Controls;

namespace SvgStudio.Mobile.Core.UI
{
    public partial class MainPage : ContentPage
    {
        string svgFlag = "<svg xmlns=\"http://www.w3.org/2000/svg\" height=\"30\" width=\"60\" version=\"1.1\" xmlns:xlink=\"http://www.w3.org/1999/xlink\"><g><clipPath id=\"t\"><path d=\"m30 15h30v15zv15h-30zh-30v-15zv-15h30z\"></path></clipPath><path data-fill-index=\"0\" fill=\"#00247d\" d=\"m0 0v30h60v-30z\"></path><path data-stroke-index=\"0\" stroke-width=\"6\" stroke=\"#fff\" d=\"m0 0 60 30m0-30-60 30\"></path><path data-stroke-index=\"1\" stroke=\"#cf142b\" stroke-width=\"4\" clip-path=\"url(#t)\" d=\"m0 0 60 30m0-30-60 30\"></path><path data-stroke-index=\"0\" stroke-width=\"10\" stroke=\"#fff\" d=\"m30 0v30m-30-15h60\"></path><path data-stroke-index=\"1\" stroke-width=\"6\" stroke=\"#cf142b\" d=\"m30 0v30m-30-15h60\"></path></g></svg>";
        string svgBear = null;
        string svgDrawing = null;

        public MainPage()
        {
            InitializeComponent();
            MyButton.Clicked += Button_Clicked;
            svgBear = GetBear();
            svgDrawing = GetDrawing();
        }

        Random random = new Random();

        private void Button_Clicked(object sender, EventArgs args)
        {
            var button = (Button)sender;
            button.Text = "Sync...";
            //string endpoint = "http://192.168.1.14:14501/";
            //string endpoint = "http://172.16.17.166:14501/";
            //string endpoint = "http://svgstudio.azurewebsites.net/";
            //var sync = new DatabaseSynchronizer(DependencyService.Get<IDatabaseConnectionProvider>(), new MobileServiceGateway(endpoint));
            //Task.Run(sync.SynchronizeModelWithServer)
            //    .ContinueWith(result =>
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            if (result != null)
            //            {
            //                SummaryLabel.Text = string.Join(Environment.NewLine, result.Result.Select(x => x.ToString()));
            //                button.Text = "Done";
            //            }
            //            else
            //            {
            //                button.Text = "Error";
            //            }
            //        });
            //    });


            //var db = DependencyService.Get<IDatabaseConnectionProvider>().GetConnection();

            int rnd = random.Next(1, 4);
            string markup = null;
            if (rnd % 3 == 0)
            {
                markup = svgFlag;
            }
            else if (rnd % 3 == 1)
            {
                markup = svgBear;
            }
            else
            {
                markup = svgDrawing;
            }
			markup = svgDrawing;
            //TestImage.WidthRequest = 300;
            //TestImage.HeightRequest = 500;
            //TestImage.SvgMarkup = markup;
            Wrap.Children.Add(new SvgImage { SvgMarkup = markup, WidthRequest = 300, HeightRequest = 500 });
        }

        //int i = 0;

        private string GetBear()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            using (var resourceStream = assembly.GetManifestResourceStream("SvgStudio.Mobile.Core.Bear.svg"))
            using (var reader = new StreamReader(resourceStream))
            {
                return reader.ReadToEnd();
            }
        }

        private string GetDrawing()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            using (var resourceStream = assembly.GetManifestResourceStream("SvgStudio.Mobile.Core.drawing.svg"))
            using (var reader = new StreamReader(resourceStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}