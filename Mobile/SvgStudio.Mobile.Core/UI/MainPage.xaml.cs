using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Mobile.Core.Models.Storage;

namespace SvgStudio.Mobile.Core.UI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MyButton.Clicked += async (sender, args) =>
            {
                await Button_Clicked(sender, args);
            };
        }

        int i = 0;

        private async Task Button_Clicked(object sender, EventArgs args)
        {
            var button = (Button)sender;
            button.Text = "Sync...";

			var sync = new ModelSynchronizer(DependencyService.Get<IDatabaseConnectionProvider>());
            var response = await sync.SynchronizeModelWithServer();

            string summary = string.Format("added: {0}, updated: {1}, deleted: {2}", response.TemplateChanges.Added.Count, response.TemplateChanges.Updated.Count, response.TemplateChanges.Deleted.Count);
            button.Text = summary;
        }
    }
}
