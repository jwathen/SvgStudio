using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class CompatibilityTag : ISyncableRecord
    {
        [PrimaryKey]
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual string Tag { get; set; }

        [Ignore, JsonIgnore]
        public virtual ICollection<Shape_CompatibilityTag> Shape_CompatibilityTags { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<DesignRegion_CompatibilityTag> DesignRegion_CompatibilityTags { get; set; }        
    }
}
