﻿using SvgStudio.Shared.Helpers;
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
                markup.AppendLine("<g xmlns:xlink=\"http://www.w3.org/1999/xlink\">");
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

            XNamespace xlink = "http://www.w3.org/1999/xlink";
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

                var xlinkHrefAttr = element.Attribute(xlink + "href");
                if (xlinkHrefAttr != null)
                {
                    PrefixHrefWithShapeName(xlinkHrefAttr);
                }
            }

            if (shape.Descendants("defs").Any())
            {
                foreach (var defs in shape.Descendants("defs").ToList())
                {
                    defs.Remove();
                    foreach (var def in defs.Elements())
                    {
                        var idAttr = def.Attribute("id");
                        if (idAttr != null)
                        {
                            PrefixIdWithShapeName(idAttr);
                        }
                        result.Defs.Add(def);
                    }
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
            string id = attr.Value;
            id = string.Format("{0}_{1}", StringHelper.StripNonAlphaNumericChars(this.Name), attr.Value);
            attr.Value = id;
        }
    }
}
