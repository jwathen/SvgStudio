using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class DesignRegion_CompatibilityTag : ISyncableRecord
    {
        [PrimaryKey]
        public string Id { get; set; }

        [Indexed(Name = "IX_DesignRegion_CompatibilityTag_X", Order = 1)]
        public string CompatibilityTagId { get; set; }
        [Indexed(Name = "IX_DesignRegion_CompatibilityTag_X", Order = 2)]
        public string DesignRegionId { get; set; }
        public byte[] RowVersion
        {
            get
            {
                string id = this.Id;
                if (id == null)
                {
                    return new byte[0];
                }
                else
                {
                    return Encoding.UTF8.GetBytes(id);
                }
            }
        }

        [Ignore, JsonIgnore]
        public DesignRegion DesignRegion { get; set; }
        [Ignore, JsonIgnore]
        public CompatibilityTag CompatibilityTag { get; set; }

        public void ComputeId()
        {
            string compatibilityTagId = CompatibilityTagId;
            if (compatibilityTagId == null && CompatibilityTag != null)
            {
                compatibilityTagId = CompatibilityTag.Id;
            }

            string designRegionId = DesignRegionId;
            if (designRegionId == null && DesignRegion != null)
            {
                designRegionId = DesignRegion.Id;
            }

            string id = null;
            if (!string.IsNullOrWhiteSpace(compatibilityTagId) && !string.IsNullOrWhiteSpace(designRegionId))
            {
                id = string.Format("{0}|{1}", compatibilityTagId, designRegionId);
            }

            this.Id = id;
        }
    }
}
