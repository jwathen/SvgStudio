using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvgStudio.Shared.Drawing
{
    public class Design
    {
        public Design()
        {
            Palette = new Palette();
        }

        public Shape Shape { get; set; }
        public Palette Palette { get; set; }

        public RenderDesignResult Render()
        {
            return Shape.Render(Palette);
        }
    }
}
