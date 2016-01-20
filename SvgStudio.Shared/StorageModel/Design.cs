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
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        [Indexed]
        public string ShapeId { get; set; }
        [Indexed]
        public string PaletteId { get; set; }

        [Ignore, JsonIgnore]
        public Shape Shape { get; set; }
        [Ignore, JsonIgnore]
        public Palette Palette { get; set; }
        [Ignore, JsonIgnore]
        public ICollection<Fill> Fills { get; set; }
    }
}
