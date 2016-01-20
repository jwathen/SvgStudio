using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Shared.StorageModel
{
    public class Fill : ISyncableRecord
    {
        [PrimaryKey]
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public FillType FillType { get; set; }
        [Indexed]
        public string PaletteId { get; set; }

        public string SolidColorFill_Color { get; set; }

        public string PatternFill_Name { get; set; }
        public int? PatternFill_X { get; set; }
        public int? PatternFill_Y { get; set; }
        public double? PatternFill_Width { get; set; }
        public double? PatternFill_Height { get; set; }
        public string PatternFill_PatternUnits { get; set; }
        public string PatternFill_PatternContentUnits { get; set; }
        [Indexed]
        public string PatternFill_DesignId { get; set; }
    }
}
