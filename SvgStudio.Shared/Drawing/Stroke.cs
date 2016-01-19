using SvgStudio.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class Stroke
    {
        public Color Color { get; set; }

        public int Width { get; set; }

        public int[] DashArray { get; set; }

        public void ApplyTo(XElement target)
        {
            var strokeAttr = target.Attribute("stroke");
            if (Color == null)
            {
                if (strokeAttr != null)
                {
                    strokeAttr.Remove();
                }
            }
            else
            {
                if (strokeAttr == null)
                {
                    strokeAttr = new XAttribute("stroke", string.Empty);
                    target.Add(strokeAttr);
                }
                strokeAttr.Value = Color.ToString();
            }

            var widthAttr = target.Attribute("stroke-width");
            if (widthAttr == null)
            {
                widthAttr = new XAttribute("stroke-width", string.Empty);
                target.Add(widthAttr);
            }
            widthAttr.Value = Width.ToString();

            var dashArrayAttr = target.Attribute("stroke-dasharray");
            if (DashArray == null)
            {
                if (dashArrayAttr != null)
                {
                    dashArrayAttr.Remove();
                }
            }
            else
            {
                if (dashArrayAttr == null)
                {
                    dashArrayAttr = new XAttribute("stroke-dasharry", string.Empty);
                    target.Add(dashArrayAttr);
                }
                dashArrayAttr.Value = string.Join(",", DashArray);
            }
        }
    }
}
