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
using SvgStudio.Shared.Materializer;
using System.Xml.Linq;
using SvgStudio.Shared.Helpers;
using System.Diagnostics;
using SvgStudio.Shared;
using SvgStudio.Mobile.Core.ViewModels;

namespace SvgStudio.Mobile.Core.UI
{
    public partial class MainPage : ContentPage
    {
        private readonly IStorageRepository _db;

        public MainPage(IStorageRepository db)
        {
            InitializeComponent();
            _db = db;
            SyncToolbarItem.Clicked += SyncToolbarItem_Clicked;
            DoIt();
        }

        public void DoIt()
        {
            Task.Factory.StartNew(() =>
            {
                var vm = new StudioViewModel("16284653806660-fda6c4c1ae034e5ea", StepView, _db);
                vm.Init();
                return vm;
            }).ContinueWith(task =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    BindingContext = task.Result;
                    task.Result.Steps.First(x => x.DisplayText.StartsWith("Left")).Start(StepView);
                });
            });
        }

        private void SyncToolbarItem_Clicked(object sender, EventArgs e)
        {
            //string endpoint = "http://192.168.1.14:14501/";
            //string endpoint = "http://172.16.17.166:14501/";
            string endpoint = "http://svgstudio.azurewebsites.net/";
            var sync = new DatabaseSynchronizer(DependencyService.Get<IDatabaseConnectionProvider>(), new MobileServiceGateway(endpoint));
            Task.Run(sync.SynchronizeModelWithServer)
                .ContinueWith(result =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (result != null)
                        {
                            string message = string.Join(Environment.NewLine, result.Result.Select(x => x.ToString()));
                            DisplayAlert("Complete", message, "OK");
                        }
                        else
                        {
                            DisplayAlert("Alert", "Error", "OK");
                        }
                    });
                });
        }
    }
}