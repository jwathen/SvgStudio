using SvgStudio.Shared.Helpers;
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
                return XElement.Parse(_markupFragmentAccessor(_markupFragmentId));
            }
        }

        public override RenderDesignResult Render(Palette palette)
        {
            RenderDesignResult result = new RenderDesignResult();
            result.Width = this.Width;
            result.Height = this.Height;

            XElement shape = new XElement(this.Markup);

            foreach (var element in shape.DescendantsAndSelf())
            {
                if (palette != null)
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

                var xlinkHrefAttr = element.Attribute(xmlns.xlink + "href");
                if (xlinkHrefAttr != null)
                {
                    PrefixHrefWithShapeName(xlinkHrefAttr);
                }

                foreach (var urlRefAttr in element.Attributes().Where(x => x.Value.StartsWith("url(#")))
                {
                    string referencedId = urlRefAttr.Value.Replace("url(#", string.Empty).Replace(")", string.Empty);
                    urlRefAttr.Value = "url(#" + PrefixIdWithShapeName(referencedId) + ")";
                }
            }

            foreach (var defs in shape.Descendants(xmlns.svg + "defs").ToList())
            {
                defs.Remove();
                foreach (var def in defs.Elements())
                {
                    var idAttr = def.Attribute(xmlns.svg + "id") ?? def.Attribute("id");
                    if (idAttr != null)
                    {
                        PrefixIdWithShapeName(idAttr);
                    }
                    result.Defs.Add(def);
                }
            }

            result.Xml = shape;

            return result;
        }

        private void PrefixHrefWithShapeName(XAttribute attr)
        {
            string href = attr.Value;
            href = string.Format("#{0}_{1}",
                StringHelper.StripNonAlphaNumericChars(this.Name),
                attr.Value.Substring(1, attr.Value.Length - 1));
            attr.Value = href;
        }

        private void PrefixIdWithShapeName(XAttribute attr)
        {
            attr.Value = PrefixIdWithShapeName(attr.Value);
        }

        private string PrefixIdWithShapeName(string id)
        {
            return string.Format("{0}_{1}", StringHelper.StripNonAlphaNumericChars(this.Name), id);
        }
    }
}
