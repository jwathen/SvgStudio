using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class CompatibilityTag
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Tag { get; set; }

        public ICollection<Shape> Shapes { get; set; }
        public ICollection<DesignRegion> DesignRegions { get; set; }
    }
}