using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvgStudio.Web.Models
{
    public class PatternFill : Fill
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string PatternUnits { get; set; }
        public string PatternContentUnits { get; set; }
        public int DesignId { get; set; }

        public Design Design { get; set; }
    }
}