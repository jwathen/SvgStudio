using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Stroke : ISyncableRecord
    {
        [PrimaryKey]
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Color { get; set; }
        public virtual int Width { get; set; }
        public virtual string PaletteId { get; set; }

        [Ignore, JsonIgnore]
        public virtual Palette Palette { get; set; }
    }
}
