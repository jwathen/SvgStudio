using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public abstract class Fill
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }

        public int? PaletteId { get; set; }

        public Palette Palette { get; set; }
    }
}