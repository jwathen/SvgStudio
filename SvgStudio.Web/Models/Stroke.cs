using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class Stroke
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public string DashArray { get; set; }

        public int? PaletteId { get; set; }

        public Palette Palette { get; set; }
    }
}