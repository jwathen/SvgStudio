using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class Design
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public int ShapeId { get; set; }
        public int PaletteId { get; set; }

        public Shape Shape { get; set; }
        public Palette Palette { get; set; }
    }
}