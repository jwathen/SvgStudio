using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class RenderDesignResult
    {
        public HashSet<DefObject> Defs { get; set; } = new HashSet<DefObject>();
        public XElement Xml { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
