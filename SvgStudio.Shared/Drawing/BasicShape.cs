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
        private readonly string _markupFragmentId;
        private readonly Func<string, string> _markupFragmentAccessor;

        public BasicShape(
            double width,
            double height,
            string markupFragmentId,
            Func<string, string> markupFragmentAccessor)
        {
            Width = width;
            Height = height;
            _markupFragmentId = markupFragmentId;
            _markupFragmentAccessor = markupFragmentAccessor;
        }

        public XElement Markup
        {
            get
            {
                StringBuilder markup = new StringBuilder();
                markup.AppendLine("<g>");
                markup.Append(_markupFragmentAccessor(_markupFragmentId));
                markup.AppendLine("</g>");
                return XElement.Parse(markup.ToString());
            }
        }

        public override RenderDesignResult Render(Palette palette)
        {
            RenderDesignResult result = new RenderDesignResult();
            result.Width = this.Width;
            result.Height = this.Height;

            XElement shape = new XElement(this.Markup);

            if (palette != null)
            {
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
            }

            result.Xml = shape;

            return result;
        }
    }
}
