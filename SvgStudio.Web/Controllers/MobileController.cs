using SvgStudio.Shared.ServiceContracts.Requests;
using SvgStudio.Shared.ServiceContracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SvgStudio.Web.Models;
using SvgStudio.Shared.ServiceContracts.Entities;
using SvgStudio.Shared.Helpers;
using Dapper;

namespace SvgStudio.Web.Controllers
{
    [RoutePrefix("Mobile")]
    public class MobileController : SvgStudioControllerBase
    {
        [HttpGet]
        [Route("GetVersion")]
        public ActionResult GetVersion()
        {
            return Json("1.0.0");
        }

        [HttpPost]
        [Route("Sync")]
        public async Task<JsonResult> Sync(MobileSyncRequest request)
        {
            MobileSyncResponse response = new MobileSyncResponse();

            // Compatibility tags
            var serverCompatibilityTags = await db.CompatibilityTags.ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<CompatibilityTagDto>)x);
            response.CompatibilityTagChanges = DetectServerChanges(serverCompatibilityTags, ConvertDictionaryKeysToInts(request.CompatibilityTagRowVersions));

            // Content licenses
            var serverContentLicenses = await db.ContentLicenses.ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<ContentLicenseDto>)x);
            response.ContentLicenseChanges = DetectServerChanges(serverContentLicenses, ConvertDictionaryKeysToInts(request.ContentLicenseRowVersions));

            // Designs
            var serverDesigns = await db.Designs.ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<DesignDto>)x);
            response.DesignChanges = DetectServerChanges(serverDesigns, ConvertDictionaryKeysToInts(request.DesignRowVersions));

            // Design regions
            var serverDesignRegions = await db.DesignRegions.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<DesignRegionDto>)x);
            response.DesignRegionChanges = DetectServerChanges(serverDesignRegions, ConvertDictionaryKeysToInts(request.DesignRegionRowVersions));

            // Fills
            var serverFills = await db.Fills.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<FillDto>)x);
            response.FillChanges = DetectServerChanges(serverFills, ConvertDictionaryKeysToInts(request.FillRowVersions));

            // Licenses
            var serverLicenses = await db.Licenses.ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<LicenseDto>)x);
            response.LicenseChanges = DetectServerChanges(serverLicenses, ConvertDictionaryKeysToInts(request.LicenseRowVersions));

            // Markup fragments
            var serverMarkupFragments = await db.MarkupFragments.ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<MarkupFragmentDto>)x);
            response.MarkupFragmentChanges = DetectServerChanges(serverMarkupFragments, ConvertDictionaryKeysToInts(request.MarkupFragmentRowVersions));

            // Palettes
            var serverPalettes = await db.Palettes.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<PaletteDto>)x);
            response.PaletteChanges = DetectServerChanges(serverPalettes, ConvertDictionaryKeysToInts(request.PaletteRowVersions));

            // Shapes
            var serverShapes = await db.Shapes.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<ShapeDto>)x);
            response.ShapeChanges = DetectServerChanges(serverShapes, ConvertDictionaryKeysToInts(request.ShapeRowVersions));

            // Strokes
            var serverStrokes = await db.Strokes.ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<StrokeDto>)x);
            response.StrokeChanges = DetectServerChanges(serverStrokes, ConvertDictionaryKeysToInts(request.StrokeRowVersions));

            // Templates
            var serverTemplates = await db.Templates.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<TemplateDto>)x);
            response.TemplateChanges = DetectServerChanges(serverTemplates, ConvertDictionaryKeysToInts(request.TemplateRowVersions));

            // DesignRegion_CompatibilityTag rows
            await db.Database.Connection.OpenAsync();
            var serverDesignRegion_CompatibilityTags = (await db.Database.Connection.QueryAsync<DesignRegion_CompatibilityTagDto>("select CompatibilityTagId, DesignRegionId from DesignRegion_CompatibilityTag")).ToArray();
            var serverDesignRegion_CompatibilityTagIds = serverDesignRegion_CompatibilityTags.Select(x => x.GetUniqueId()).ToArray();
            var mobileDesignRegion_CompatibilityTags = request.DesignRegion_CompatibilityTags.Select(x => x.GetUniqueId()).ToArray();
            response.DesignRegion_CompatibilityTagChanges.Added = serverDesignRegion_CompatibilityTags
                .Where(x => !mobileDesignRegion_CompatibilityTags.Contains(x.GetUniqueId()))
                .ToList();
            response.DesignRegion_CompatibilityTagChanges.Deleted = request.DesignRegion_CompatibilityTags
                .Where(x => !serverDesignRegion_CompatibilityTagIds.Contains(x.GetUniqueId()))
                .ToList();

            // Shape_CompatibilityTag rows
            var serverShape_CompatibilityTags = (await db.Database.Connection.QueryAsync<Shape_CompatibilityTagDto>("select CompatibilityTagId, ShapeId from Shape_CompatibilityTag")).ToArray();
            var serverShape_CompatibilityTagIds = serverShape_CompatibilityTags.Select(x => x.GetUniqueId()).ToArray();
            var mobileShape_CompatibilityTags = request.Shape_CompatibilityTags.Select(x => x.GetUniqueId()).ToArray();
            response.Shape_CompatibilityTagChanges.Added = serverShape_CompatibilityTags
                .Where(x => !mobileShape_CompatibilityTags.Contains(x.GetUniqueId()))
                .ToList();
            response.Shape_CompatibilityTagChanges.Deleted = request.Shape_CompatibilityTags
                .Where(x => !serverShape_CompatibilityTagIds.Contains(x.GetUniqueId()))
                .ToList();
            db.Database.Connection.Close();

            return Json(response);
        }

        private EntityChangeData<T> DetectServerChanges<T>(Dictionary<int, Models.ISyncableEntity<T>> serverData, Dictionary<int, string> clientRowVersions)
        {
            var result = new EntityChangeData<T>();

            int[] added = serverData.Keys.Except(clientRowVersions.Keys).ToArray();
            foreach (int id in added)
            {
                result.Added.Add(serverData[id].ToDto());
            }

            int[] same = serverData.Keys.Intersect(clientRowVersions.Keys).ToArray();
            foreach (int id in same)
            {
                string serverRowVersion = HexHelper.ByteArrayToHexString(serverData[id].RowVersion);
                string clientRowVersion = clientRowVersions[id];
                if (serverRowVersion != clientRowVersion)
                {
                    result.Updated.Add(serverData[id].ToDto());
                }
            }

            int[] removed = clientRowVersions.Keys.Except(serverData.Keys).ToArray();
            foreach (int id in removed)
            {
                result.Deleted.Add(id);
            }

            return result;
        }

        private Dictionary<int, string> ConvertDictionaryKeysToInts(Dictionary<string, string> dict)
        {
            dict = dict ?? new Dictionary<string, string>();
            return dict.ToDictionary(x => int.Parse(x.Key), x => x.Value);
        }
    }
}