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
            var connectionProvider = DependencyService.Get<IDatabaseConnectionProvider>();
            var connection = connectionProvider.GetConnection();
            DatabaseInitilizer.Init(connection);
            MainPage = new NavigationPage(new MainPage(new MobileStorageRepository(connection)));
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
