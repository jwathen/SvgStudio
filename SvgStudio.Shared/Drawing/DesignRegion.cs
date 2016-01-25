using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class DesignRegion
    {
        public DesignRegion(string name, int x, int y, int width, int height)
        {
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public string StorageId { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public short SortOrder { get; set; }

        public Design BuildPlaceholder()
        {
            string xml = BuildPlaceholderXml();
            var shape = new BasicShape(Width, Height, null, (x) => xml);

            return new Design { Shape = shape, Palette = null };
        }

        public string BuildPlaceholderXml()
        {
           return string.Format("<rect transform=\"translate({0},{1})\" width=\"{2}\" height=\"{3}\" stroke=\"gray\" stroke-width=\"3\" stroke-dasharray=\"5,5\" fill=\"none\" />", 
               X,
               Y,
               Width, 
               Height);
        }
    }
}
