using SvgStudio.Mobile.Core.Models;
using SvgStudio.Mobile.Core.Models.Storage;
using SvgStudio.Mobile.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SvgStudio
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new MainPage();

            var connectionProvider = DependencyService.Get<IDatabaseConnectionProvider>();
            var connection = connectionProvider.GetConnection();
            connection.CreateTable<License>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
