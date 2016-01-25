using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgStudio.Mobile.Core.Exceptions
{
    public class ShapeRenderException : Exception
    {
        public ShapeRenderException(Exception inner, string shapeId, string paletteId)
            : base("Error rendering shape.", inner)
        {
            ShapeId = shapeId;
            PaletteId = paletteId;
        }

        public string ShapeId { get; private set; }
        public string PaletteId { get; private set; }
    }
}
