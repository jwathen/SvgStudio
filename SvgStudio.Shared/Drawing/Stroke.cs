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
        public static Stroke FromStorageModel(StorageModel.Stroke stroke)
        {
            return new Stroke
            {
                Color = Color.FromName(stroke.Color),
                Width = stroke.Width
            };
        }

        public string StorageId { get; set; }
        public Color Color { get; set; }

        public int Width { get; set; }

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
                widthAttr = new XAttribute("stroke-width", Width);
                target.Add(widthAttr);
            }
        }
    }
}
