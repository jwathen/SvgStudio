using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Design : ISyncableRecord
    {
        [PrimaryKey]
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        [Indexed]
        public virtual string ShapeId { get; set; }
        [Indexed]
        public virtual string PaletteId { get; set; }

        [Ignore, JsonIgnore]
        public virtual Shape Shape { get; set; }
        [Ignore, JsonIgnore]
        public virtual Palette Palette { get; set; }
        [Ignore, JsonIgnore]
        public virtual ICollection<Fill> Fills { get; set; }
    }
}
