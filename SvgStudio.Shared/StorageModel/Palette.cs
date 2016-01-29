using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Palette : ISyncableRecord
    {
        [PrimaryKey]
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Name { get; set; }
        public virtual short SortOrder { get; set; }

        [Ignore, JsonIgnore]
        public virtual ICollection<Design> Designs { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<Fill> Fills { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<Stroke> Strokes { get; set; }
    }
}
