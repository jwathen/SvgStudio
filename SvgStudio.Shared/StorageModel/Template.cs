using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Template : ISyncableRecord
    {
        [PrimaryKey]
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public bool IsMaster { get; set; }
        public string Name { get; set; }

        [Ignore, JsonIgnore]
        public ICollection<DesignRegion> DesignRegions { get; set; }
        [Ignore, JsonIgnore]
        public ICollection<Shape> TemplateShapes { get; set; }
    }
}
