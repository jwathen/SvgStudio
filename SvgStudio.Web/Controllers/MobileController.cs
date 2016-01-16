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
        public async Task<ActionResult> Sync(MobileSyncRequest request)
        {
            MobileSyncResponse response = new MobileSyncResponse();

            var serverTemplates = await db.Templates.Where(x => x.IsActive).ToDictionaryAsync(x => x.Id, x => (ISyncableEntity<TemplateDto>)x);
            response.TemplateChanges = DetectServerChanges(serverTemplates, ConvertDictionaryKeysToInts(request.TemplateRowVersions));

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
                string serverRowVersion = serverData[id].SyncableEntityId.RowVersion;
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
            return dict.ToDictionary(x => int.Parse(x.Key), x => x.Value);
        }
    }
}