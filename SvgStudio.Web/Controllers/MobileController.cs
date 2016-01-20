using SvgStudio.Shared.ServiceContracts.Requests;
using SvgStudio.Shared.ServiceContracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SvgStudio.Shared.Helpers;
using System.Data.Entity;
using SvgStudio.Shared.StorageModel;

namespace SvgStudio.Web.Controllers
{
    [RoutePrefix("Mobile")]
    public partial class MobileController : SvgStudioControllerBase
    {
        [HttpGet]
        [Route("GetVersion")]
        public virtual ActionResult GetVersion()
        {
            return Json("1.0.0");
        }

        [HttpPost]
        [Route("Sync")]
        public virtual async Task<JsonResult> Sync(MobileSyncRequest request)
        {
            MobileSyncResponse response = new MobileSyncResponse();

            // Compatibility tags
            var serverCompatibilityTags = await db.CompatibilityTags.ToDictionaryAsync(x => x.Id);
            response.CompatibilityTagChanges = DetectServerChanges(serverCompatibilityTags, request.CompatibilityTagRowVersions);

            // Content licenses
            var serverContentLicenses = await db.ContentLicenses.ToDictionaryAsync(x => x.Id);
            response.ContentLicenseChanges = DetectServerChanges(serverContentLicenses, request.ContentLicenseRowVersions);

            // Designs
            var serverDesigns = await db.Designs.ToDictionaryAsync(x => x.Id);
            response.DesignChanges = DetectServerChanges(serverDesigns, request.DesignRowVersions);

            // Design regions
            var serverDesignRegions = await db.DesignRegions.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id);
            response.DesignRegionChanges = DetectServerChanges(serverDesignRegions, request.DesignRegionRowVersions);

            // DesignRegion_CompatibilityTags
            var serverDesignRegion_CompatibilityTags = await db.DesignRegion_CompatibilityTags.ToDictionaryAsync(x => x.Id);
            response.DesignRegion_CompatibilityTagChanges = DetectServerChanges(serverDesignRegion_CompatibilityTags, request.DesignRegion_CompatibilityTagRowVersions);

            // Fills
            var serverFills = await db.Fills.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id);
            response.FillChanges = DetectServerChanges(serverFills, request.FillRowVersions);

            // Licenses
            var serverLicenses = await db.Licenses.ToDictionaryAsync(x => x.Id);
            response.LicenseChanges = DetectServerChanges(serverLicenses, request.LicenseRowVersions);

            // Markup fragments
            var serverMarkupFragments = await db.MarkupFragments.ToDictionaryAsync(x => x.Id);
            response.MarkupFragmentChanges = DetectServerChanges(serverMarkupFragments, request.MarkupFragmentRowVersions);

            // Palettes
            var serverPalettes = await db.Palettes.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id);
            response.PaletteChanges = DetectServerChanges(serverPalettes, request.PaletteRowVersions);

            // Shapes
            var serverShapes = await db.Shapes.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id);
            response.ShapeChanges = DetectServerChanges(serverShapes, request.ShapeRowVersions);

            // Shape_CompatibilityTags
            var serverShape_CompatibilityTags = await db.Shape_CompatibilityTags.ToDictionaryAsync(x => x.Id);
            response.Shape_CompatibilityTagChanges = DetectServerChanges(serverShape_CompatibilityTags, request.Shape_CompatibilityTagRowVersions);

            // Strokes
            var serverStrokes = await db.Strokes.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id);
            response.StrokeChanges = DetectServerChanges(serverStrokes, request.StrokeRowVersions);

            // Templates
            var serverTemplates = await db.Templates.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id);
            response.TemplateChanges = DetectServerChanges(serverTemplates, request.TemplateRowVersions);

            return Json(response);
        }

        private EntityChangeData<T> DetectServerChanges<T>(Dictionary<string, T> serverRecords, Dictionary<string, byte[]> mobileRowVersions) where T : ISyncableRecord
        {
            var result = new EntityChangeData<T>();

            string[] added = serverRecords.Keys.Except(mobileRowVersions.Keys).ToArray();
            foreach (string id in added)
            {
                result.Added.Add(serverRecords[id]);
            }

            string[] alreadyExists = serverRecords.Keys.Intersect(mobileRowVersions.Keys).ToArray();
            foreach (string id in alreadyExists)
            {
                byte[] serverRowVersion = serverRecords[id].RowVersion;
                byte[] mobileRowVersion = mobileRowVersions[id];
                if (!serverRowVersion.SequenceEqual(mobileRowVersion))
                {
                    result.Updated.Add(serverRecords[id]);
                }
            }

            string[] removed = mobileRowVersions.Keys.Except(serverRecords.Keys).ToArray();
            foreach (string id in removed)
            {
                result.Deleted.Add(id);
            }

            return result;
        }
    }
}