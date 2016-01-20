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
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [Indexed]
        public string TemplateId { get; set; }
        public short SortOrder { get; set; }

        [Ignore, JsonIgnore]
        public Template Template { get; set; }
        [Ignore, JsonIgnore]
        public ICollection<DesignRegion_CompatibilityTag> DesignRegion_CompatibilityTags { get; set; }
    }
}
