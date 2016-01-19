using SvgStudio.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
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

        public Design Design { get; set; }

        public string Id
        {
            get
            {
                return string.Format("PatternFill_{0}", StringHelper.StripNonAlphaNumericChars(Name));
            }
        }

        public override DefinitionCollection GetDefs()
        {
            XElement patternEl = new XElement("pattern");
            patternEl.Add(new XAttribute("id", this.Id));
            patternEl.Add(new XAttribute("x", this.X));
            patternEl.Add(new XAttribute("y", this.Y));
            patternEl.Add(new XAttribute("width", this.Width));
            patternEl.Add(new XAttribute("height", this.Height));
            if (!string.IsNullOrWhiteSpace(PatternUnits))
            {
                patternEl.Add(new XAttribute("patternUnits", this.PatternUnits));
            }
            if (!string.IsNullOrWhiteSpace(PatternContentUnits))
            {
                patternEl.Add(new XAttribute("patternContentUnits", this.PatternContentUnits));
            }

            DefinitionCollection result = new DefinitionCollection();
            result.Add(patternEl);

            var renderedDesign = Design.Render();
            patternEl.Add(renderedDesign.Xml);
            result.Add(renderedDesign.Defs);
            return result;
        }

        public override void ApplyTo(XElement target)
        {
            var fillAttr = target.Attribute("fill");
            if (fillAttr == null)
            {
                fillAttr = new XAttribute("fill", string.Empty);
                target.Add(fillAttr);
            }

            fillAttr.Value = string.Format("url(#{0})", Id);
        }
    }
}
