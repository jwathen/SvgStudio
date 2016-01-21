using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public abstract class Shape
    {
        public string StorageId { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public abstract RenderDesignResult Render(Palette palette);
    }
}
