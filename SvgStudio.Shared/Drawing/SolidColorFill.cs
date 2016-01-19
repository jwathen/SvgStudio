using SvgStudio.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class SolidColorFill : Fill
    {
        public Color Color { get; private set; }

        public SolidColorFill(Color color)
        {
            Color = color;
        }

        public static SolidColorFill Transparent
        {
            get
            {
                return new SolidColorFill(Color.Transparent);
            }
        }

        public override void ApplyTo(XElement target)
        {
            var fillAttr = target.Attribute("fill");
            if (Color == null)
            {
                if (fillAttr != null)
                {
                    fillAttr.Remove();
                }
            }
            else
            {
                if (fillAttr == null)
                {
                    fillAttr = new XAttribute("fill", string.Empty);
                    target.Add(fillAttr);
                }
                fillAttr.Value = Color.ToString();
            }
        }

        public override DefinitionCollection GetDefs()
        {
            return new DefinitionCollection();
        }
    }
}
