using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using SvgStudio.Mobile.Core.Models.Synchronization;
using SvgStudio.Mobile.Core.Models.Storage;
using SvgStudio.Shared.ServiceContracts.Responses;

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

        private async Task Button_Clicked(object sender, EventArgs args)
        {
            var button = (Button)sender;
            button.Text = "Sync...";

            var sync = new ModelSynchronizer(DependencyService.Get<IDatabaseConnectionProvider>());
            var response = await sync.SynchronizeModelWithServer();

            if (response != null)
            {
                StringBuilder summary = new StringBuilder();
                AppendToSummary(summary, response.TemplateChanges);
                AppendToSummary(summary, response.DesignRegionChanges);
                AppendToSummary(summary, response.PaletteChanges);
                SummaryLabel.Text = summary.ToString();
                button.Text = "Done";
            }
            else
            {
                button.Text = "Error";
            }
        }

        private void AppendToSummary<T>(StringBuilder summary, EntityChangeData<T> changes)
        {
            string entitySummary= string.Format("{0} - added: {1}, updated: {2}, deleted: {3}",
                typeof(T).Name,
                changes.Added.Count,
                changes.Updated.Count,
                changes.Deleted.Count);
            summary.AppendLine(entitySummary);
        }
    }
}
