using Newtonsoft.Json;
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
        public virtual string Id { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual FillType FillType { get; set; }
        [Indexed]
        public virtual string PaletteId { get; set; }

        public virtual string SolidColorFill_Color { get; set; }

        public virtual string PatternFill_Name { get; set; }
        public virtual int? PatternFill_X { get; set; }
        public virtual int? PatternFill_Y { get; set; }
        public virtual double? PatternFill_Width { get; set; }
        public virtual double? PatternFill_Height { get; set; }
        public virtual string PatternFill_PatternUnits { get; set; }
        public virtual string PatternFill_PatternContentUnits { get; set; }
        [Indexed]
        public virtual string PatternFill_DesignId { get; set; }

        [Ignore, JsonIgnore]
        public virtual Palette Palette { get; set; }
        [Ignore, JsonIgnore]
        public virtual Design PatternFill_Design { get; set; }
    }
}
