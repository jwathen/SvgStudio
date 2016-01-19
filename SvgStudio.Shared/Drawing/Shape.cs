using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public abstract class Shape
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int NumberOfFillsSupported { get; set; }

        public int NumberOfStrokesSupported { get; set; }

        public abstract RenderDesignResult Render(Palette palette);
    }
}
