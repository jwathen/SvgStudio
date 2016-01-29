using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class DesignRegion : ISyncableRecord
    {
        [PrimaryKey]
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Name { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        [Indexed]
        public virtual string TemplateId { get; set; }
        public virtual short SortOrder { get; set; }

        [Ignore, JsonIgnore]
        public virtual Template Template { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<DesignRegion_CompatibilityTag> DesignRegion_CompatibilityTags { get; set; }
    }
}
