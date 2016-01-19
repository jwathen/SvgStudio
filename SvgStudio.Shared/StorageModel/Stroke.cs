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
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public string DashArray { get; set; }
        public string PaletteId { get; set; }
    }
}
