using SQLite.Net.Async;
using SvgStudio.Mobile.Core.Models.Storage;
using SvgStudio.Shared.ServiceContracts.Entities;
using SvgStudio.Shared.ServiceContracts.Requests;
using SvgStudio.Shared.ServiceContracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SvgStudio.Mobile.Core.Models.Synchronization
{
    public class ModelSynchronizer
    {
        private readonly SQLiteAsyncConnection _db = null;

        public ModelSynchronizer(IDatabaseConnectionProvider connectionProvider)
        {
            _db = connectionProvider.GetAsyncConnection();
        }

        public async Task<MobileSyncResponse> SynchronizeModelWithServer()
        {
            MobileSyncRequest request = new MobileSyncRequest();
            var templates = await _db.QueryAsync<Template>("select Id, RowVersion from Template where Id like '1%'");
            foreach(var template in templates)
            {
                int serverId = int.Parse(EntityId.Parse(template.Id).SourceId);
                request.TemplateRowVersions[serverId.ToString()] = template.RowVersion;
            }

            var proxy = new MobileServiceGateway("http://192.168.1.14:14501/");
            var response = await proxy.Sync(request);

            await ProcessChangesFromServer<Template, TemplateDto>(response.TemplateChanges);

            return response;
        }

        private async Task ProcessChangesFromServer<TEntity, TDto>(EntityChangeData<TDto> changes) where TEntity : ISyncableEntity<TDto>, new()
        {
            foreach (var dto in changes.Added)
            {
                var entity = new TEntity();
                entity.FillFromDto(dto);
                await _db.InsertAsync(entity);
            }

            foreach (var dto in changes.Updated)
            {
                var entity = new TEntity();
                entity.FillFromDto(dto);
                await _db.UpdateAsync(entity);
            }

            foreach (int deleteEntityId in changes.Deleted)
            {
				string id = EntityId.FromServerId(deleteEntityId).ToString();
                await _db.DeleteAsync<TEntity>(id);
            }
        }
    }
}
