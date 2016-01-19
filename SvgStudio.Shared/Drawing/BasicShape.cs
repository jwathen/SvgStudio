using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class BasicShape : Shape
    {
        public BasicShape(
            int width,
            int height,
            int numberOfFillsSupported,
            int numberOfStrokesSupported,
            string xml)
        {
            Width = width;
            Height = height;
            NumberOfFillsSupported = numberOfFillsSupported;
            NumberOfStrokesSupported = numberOfStrokesSupported;
            Markup = XElement.Parse(xml);
        }

        public XElement Markup { get; set; }

        public override RenderDesignResult Render(Palette palette)
        {
            RenderDesignResult result = new RenderDesignResult();
            result.Width = this.Width;
            result.Height = this.Height;

            XElement shape = new XElement(this.Markup);

            foreach (var element in shape.DescendantsAndSelf())
            {
                var strokeIndexAttr = element.Attribute("data-stroke-index");
                if (strokeIndexAttr != null)
                {
                    int index = int.Parse(strokeIndexAttr.Value);
                    palette.GetStroke(index).ApplyTo(element);
                    strokeIndexAttr.Remove();
                }

                var fillIndexAttr = element.Attribute("data-fill-index");
                if (fillIndexAttr != null)
                {
                    int index = int.Parse(fillIndexAttr.Value);
                    var fill = palette.GetFill(index);
                    fill.ApplyTo(element);
                    result.Defs.Add(fill.GetDefs());
                    fillIndexAttr.Remove();
                }
            }

            result.Xml = shape;

            return result;
        }
    }
}
