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
using SvgStudio.Mobile.Core.Services;
using System.Collections;

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
            request.CompatibilityTagRowVersions = await GetLocalRowVerionsAsync<CompatibilityTag, CompatibilityTagDto>();
            request.ContentLicenseRowVersions = await GetLocalRowVerionsAsync<ContentLicense, ContentLicenseDto>();
            request.DesignRowVersions = await GetLocalRowVerionsAsync<Design, DesignDto>();
            request.DesignRegionRowVersions = await GetLocalRowVerionsAsync<DesignRegion, DesignRegionDto>();
            request.FillRowVersions = await GetLocalRowVerionsAsync<Fill, FillDto>();
            request.LicenseRowVersions = await GetLocalRowVerionsAsync<License, LicenseDto>();
            request.MarkupFragmentRowVersions = await GetLocalRowVerionsAsync<MarkupFragment, MarkupFragmentDto>();
            request.PaletteRowVersions = await GetLocalRowVerionsAsync<Palette, PaletteDto>();
            request.ShapeRowVersions = await GetLocalRowVerionsAsync<Shape, ShapeDto>();
            request.StrokeRowVersions = await GetLocalRowVerionsAsync<Stroke, StrokeDto>();
            request.TemplateRowVersions = await GetLocalRowVerionsAsync<Template, TemplateDto>();
            request.DesignRegion_CompatibilityTags = (await _db.QueryAsync<DesignRegion_CompatibilityTag>("select DesignRegionId, CompatibilityTagId from DesignRegion_CompatibilityTag where CompatibilityTagId like '1%'"))
                .Select(x => new DesignRegion_CompatibilityTagDto
                {
                    CompatibilityTagId = int.Parse(EntityId.Parse(x.CompatibilityTagId).SourceId),
                    DesignRegionId = int.Parse(EntityId.Parse(x.DesignRegionId).SourceId)
                }).ToList();
            request.Shape_CompatibilityTags = (await _db.QueryAsync<Shape_CompatibilityTag>("select ShapeId, CompatibilityTagId from Shape_CompatibilityTag where CompatibilityTagId like '1%'"))
                .Select(x => new Shape_CompatibilityTagDto
                {
                    CompatibilityTagId = int.Parse(EntityId.Parse(x.CompatibilityTagId).SourceId),
                    ShapeId = int.Parse(EntityId.Parse(x.ShapeId).SourceId)
                }).ToList();

            var response = await _mobileService.Sync(request);

            summaries.Add(await ProcessChangesFromServer<CompatibilityTag, CompatibilityTagDto>(response.CompatibilityTagChanges));
            summaries.Add(await ProcessChangesFromServer<ContentLicense, ContentLicenseDto>(response.ContentLicenseChanges));
            summaries.Add(await ProcessChangesFromServer<Design, DesignDto>(response.DesignChanges));
            summaries.Add(await ProcessChangesFromServer<Fill, FillDto>(response.FillChanges));
            summaries.Add(await ProcessChangesFromServer<License, LicenseDto>(response.LicenseChanges));
            summaries.Add(await ProcessChangesFromServer<MarkupFragment, MarkupFragmentDto>(response.MarkupFragmentChanges));
            summaries.Add(await ProcessChangesFromServer<Palette, PaletteDto>(response.PaletteChanges));
            summaries.Add(await ProcessChangesFromServer<Shape, ShapeDto>(response.ShapeChanges));
            summaries.Add(await ProcessChangesFromServer<Stroke, StrokeDto>(response.StrokeChanges));
            summaries.Add(await ProcessChangesFromServer<Template, TemplateDto>(response.TemplateChanges));

            // DesignRegion_CompatibilityTags
            var designRegionCompatibilityTagsSummary = new TableSynchronizationSummary();
            designRegionCompatibilityTagsSummary.TableName = "DesignRegion_CompatibilityTag";
            summaries.Add(designRegionCompatibilityTagsSummary);
            foreach (var added in response.DesignRegion_CompatibilityTagChanges.Added)
            {
                var designRegionCompatibilityTag = new DesignRegion_CompatibilityTag();
                designRegionCompatibilityTag.FillFromDto(added);
                await _db.InsertAsync(designRegionCompatibilityTag);
                designRegionCompatibilityTagsSummary.RowsAdded++;
            }
            foreach (var deleted in response.DesignRegion_CompatibilityTagChanges.Deleted)
            {
                await _db.ExecuteAsync("delete DesignRegion_CompatibilityTag where DesignRegionId = ? and CompatibilityTagId = ?", deleted.DesignRegionId, deleted.CompatibilityTagId);
                designRegionCompatibilityTagsSummary.RowsUpdated++;
            }

            // Shape_CompatibilityTags
            var shapeCompatibilityTagsSummary = new TableSynchronizationSummary();
            shapeCompatibilityTagsSummary.TableName = "Shape_CompatibilityTag";
            summaries.Add(shapeCompatibilityTagsSummary);
            foreach (var added in response.Shape_CompatibilityTagChanges.Added)
            {
                var ShapeCompatibilityTag = new Shape_CompatibilityTag();
                ShapeCompatibilityTag.FillFromDto(added);
                await _db.InsertAsync(ShapeCompatibilityTag);
                shapeCompatibilityTagsSummary.RowsAdded++;
            }
            foreach (var deleted in response.Shape_CompatibilityTagChanges.Deleted)
            {
                await _db.ExecuteAsync("delete Shape_CompatibilityTag where ShapeId = ? and CompatibilityTagId = ?", deleted.ShapeId, deleted.CompatibilityTagId);
                shapeCompatibilityTagsSummary.RowsDeleted++;
            }

            return summaries;
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

        private async Task<TableSynchronizationSummary> ProcessChangesFromServer<TEntity, TDto>(EntityChangeData<TDto> changes) where TEntity : ISyncableEntity<TDto>, new()
        {
            var summary = new TableSynchronizationSummary();
            summary.TableName = typeof(TEntity).Name;

            foreach (var dto in changes.Added)
            {
                var entity = new TEntity();
                entity.FillFromDto(dto);
                await _db.InsertAsync(entity);
                summary.RowsAdded++;
            }

            foreach (var dto in changes.Updated)
            {
                var entity = new TEntity();
                entity.FillFromDto(dto);
                await _db.UpdateAsync(entity);
                summary.RowsUpdated++;
            }

            foreach (int deleteEntityId in changes.Deleted)
            {
				string id = EntityId.FromServerId(deleteEntityId).ToString();
                await _db.DeleteAsync<TEntity>(id);
                summary.RowsDeleted++;
            }

            return summary;
        }
    }
}
