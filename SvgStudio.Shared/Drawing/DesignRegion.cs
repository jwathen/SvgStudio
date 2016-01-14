using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvgStudio.Shared.Drawing
{
    public class DesignRegion
    {
        public DesignRegion(string key, int x, int y, int width, int height)
        {
            Key = key;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public string Key { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Design BuildPlaceholder()
        {
            var palette = new Palette();
            palette.Strokes.Add(new Stroke { Color = Color.FromName("DarkGray"), Width = 1, DashArray = new[] { 5, 5 } });

            string xml = string.Format("<rect width=\"{0}\" height=\"{1}\" class=\"stroke_placeholder_0 fill_placeholder_0\" />", Width, Height);
            var shape = new BasicShape(Width, Height, 1, 1, xml);

            return new Design { Shape = shape, Palette = palette };
        }
    }
}
