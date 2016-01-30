using SvgStudio.Shared.Helpers;
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
        public int NumberOfFillsSupported { get; set; }
        public int NumberOfStrokesSupported { get; set; }

        public bool CanBeRecolored()
        {
            return NumberOfFillsSupported > 0
                || NumberOfStrokesSupported > 0;
        }

        public abstract RenderDesignResult Render(Palette palette, string namingContext);
        public abstract RenderDesignResult RenderPreview();

        protected string AddNamingPrefixToId(string id, string namingContext)
        {
            return StringHelper.StripNonAlphaNumericChars(string.Format("{0}_{1}_{2}", namingContext, Name, id));
        }
    }
}
