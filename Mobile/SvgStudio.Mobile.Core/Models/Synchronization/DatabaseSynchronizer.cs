using SQLite.Net.Async;
using SvgStudio.Mobile.Core.Models.Storage;
using SvgStudio.Mobile.Core.Services;
using SvgStudio.Shared.ServiceContracts.Requests;
using SvgStudio.Shared.ServiceContracts.Responses;
using SvgStudio.Shared.StorageModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Models.Synchronization
{
    public class DatabaseSynchronizer
    {
        private readonly SQLiteAsyncConnection _db = null;
        private readonly IMobileServiceGateway _mobileService = null;

        public DatabaseSynchronizer(IDatabaseConnectionProvider connectionProvider, IMobileServiceGateway mobileService)
        {
            _db = connectionProvider.GetAsyncConnection();
            _mobileService = mobileService;
        }

        public async Task<IEnumerable<TableSynchronizationSummary>> SynchronizeModelWithServer()
        {
            var summaries = new List<TableSynchronizationSummary>();

            MobileSyncRequest request = new MobileSyncRequest();
            request.CompatibilityTagRowVersions = await GetLocalRowVerionsAsync<CompatibilityTag>();
            request.ContentLicenseRowVersions = await GetLocalRowVerionsAsync<ContentLicense>();
            request.DesignRowVersions = await GetLocalRowVerionsAsync<Design>();
            request.DesignRegionRowVersions = await GetLocalRowVerionsAsync<DesignRegion>();
            request.DesignRegion_CompatibilityTagRowVersions = 
                (await _db.QueryAsync<DesignRegion_CompatibilityTag>("select * from DesignRegion_CompatibilityTag"))
                .ToDictionary(x => x.Id, x => x.RowVersion);
            request.FillRowVersions = await GetLocalRowVerionsAsync<Fill>();
            request.LicenseRowVersions = await GetLocalRowVerionsAsync<License>();
            request.MarkupFragmentRowVersions = await GetLocalRowVerionsAsync<MarkupFragment>();
            request.PaletteRowVersions = await GetLocalRowVerionsAsync<Palette>();
            request.ShapeRowVersions = await GetLocalRowVerionsAsync<Shape>();
            request.Shape_CompatibilityTagRowVersions =
                (await _db.QueryAsync<Shape_CompatibilityTag>("select * from Shape_CompatibilityTag"))
                .ToDictionary(x => x.Id, x => x.RowVersion);
            request.StrokeRowVersions = await GetLocalRowVerionsAsync<Stroke>();
            request.TemplateRowVersions = await GetLocalRowVerionsAsync<Template>();

            var response = await _mobileService.Sync(request);

            summaries.Add(await ProcessChangesFromServer(response.CompatibilityTagChanges));
            summaries.Add(await ProcessChangesFromServer(response.ContentLicenseChanges));
            summaries.Add(await ProcessChangesFromServer(response.DesignChanges));
            summaries.Add(await ProcessChangesFromServer(response.DesignRegionChanges));
            summaries.Add(await ProcessChangesFromServer(response.DesignRegion_CompatibilityTagChanges));
            summaries.Add(await ProcessChangesFromServer(response.FillChanges));
            summaries.Add(await ProcessChangesFromServer(response.LicenseChanges));
            summaries.Add(await ProcessChangesFromServer(response.MarkupFragmentChanges));
            summaries.Add(await ProcessChangesFromServer(response.PaletteChanges));
            summaries.Add(await ProcessChangesFromServer(response.ShapeChanges));
            summaries.Add(await ProcessChangesFromServer(response.Shape_CompatibilityTagChanges));
            summaries.Add(await ProcessChangesFromServer(response.StrokeChanges));
            summaries.Add(await ProcessChangesFromServer(response.TemplateChanges));

            return summaries;
        }

        private async Task<Dictionary<string, byte[]>> GetLocalRowVerionsAsync<T>() where T : class, ISyncableRecord
        {
            string sql = string.Format("select Id, RowVersion from {0}", typeof(T).Name);
            return (await _db.QueryAsync<T>(sql)).ToDictionary(x => x.Id, x => x.RowVersion);
        }

        private async Task<TableSynchronizationSummary> ProcessChangesFromServer<T>(EntityChangeData<T> changes) where T : class
        {
            var summary = new TableSynchronizationSummary();
            summary.TableName = typeof(T).Name;

            foreach (var row in changes.Added)
            {
                await _db.InsertAsync(row);
                summary.RowsAdded++;
            }

            foreach (var row in changes.Updated)
            {
                await _db.UpdateAsync(row);
                summary.RowsUpdated++;
            }

            foreach (string id in changes.Deleted)
            {
                await _db.DeleteAsync<T>(id);
                summary.RowsDeleted++;
            }

            return summary;
        }
    }
}
