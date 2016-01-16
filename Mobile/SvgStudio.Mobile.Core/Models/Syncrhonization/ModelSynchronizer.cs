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
using Newtonsoft.Json;

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
            request.TemplateRowVersions = await GetLocalRowVerionsAsync<Template, TemplateDto>();
            request.DesignRegionRowVersions = await GetLocalRowVerionsAsync<DesignRegion, DesignRegionDto>();
            request.PaletteRowVersions = await GetLocalRowVerionsAsync<Palette, PaletteDto>();

            var proxy = new MobileServiceGateway("http://192.168.1.14:14501/");
            var response = await proxy.Sync(request);

			if(request != null)
			{
				Debug.WriteLine(JsonConvert.SerializeObject(response));

				await ProcessChangesFromServer<Template, TemplateDto>(response.TemplateChanges);
				await ProcessChangesFromServer<DesignRegion, DesignRegionDto>(response.DesignRegionChanges);
                await ProcessChangesFromServer<Palette, PaletteDto>(response.PaletteChanges);
            }

            return response;
        }

        private async Task<Dictionary<string, string>> GetLocalRowVerionsAsync<TEntity, TDto>() where TEntity : class, ISyncableEntity<TDto>
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string sql = string.Format("select Id, RowVersion from {0} where Id like '1%'", typeof(TEntity).Name);
            var localRowVersions = await _db.QueryAsync<TEntity>(sql);
            foreach (var localEntity in localRowVersions)
            {
                int serverId = int.Parse(EntityId.Parse(localEntity.Id).SourceId);
                result[serverId.ToString()] = localEntity.RowVersion;
            }

            return result;
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
