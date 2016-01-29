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
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsMaster { get; set; }
        public virtual string Name { get; set; }

        [Ignore, JsonIgnore]
        public virtual ICollection<DesignRegion> DesignRegions { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<Shape> TemplateShapes { get; set; }
    }
}
