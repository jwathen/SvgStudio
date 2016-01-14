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
        protected static Regex FILL_PLACEHOLDER_REGEX = new Regex(@"fill_placeholder_(\d+)", RegexOptions.IgnoreCase);
        protected static Regex STROKE_PLACEHOLDER_REGEX = new Regex(@"stroke_placeholder_(\d+)", RegexOptions.IgnoreCase);

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
            Xml = XElement.Parse(xml);
        }

        public XElement Xml { get; set; }

        public override RenderDesignResult Render(Palette palette)
        {
            RenderDesignResult result = new RenderDesignResult();
            result.Width = this.Width;
            result.Height = this.Height;

            Dictionary<int, string> fillCssClassNames = new Dictionary<int, string>();
            for (int i = 0; i < NumberOfFillsSupported; i++)
            {
                Fill fill = palette.GetFill(i);
                fillCssClassNames[i] = fill.CssClass;
                result.Defs.Add(fill);
            }

            Dictionary<int, string> strokeCssClassNames = new Dictionary<int, string>();
            for (int i = 0; i < NumberOfStrokesSupported; i++)
            {
                Stroke stroke = palette.GetStroke(i);
                strokeCssClassNames[i] = stroke.CssClass;
                result.Defs.Add(stroke);
            }

            XElement shape = new XElement(this.Xml);

            foreach (var element in shape.DescendantsAndSelf())
            {
                XAttribute classAttribute = element.Attribute("class");
                if (classAttribute != null)
                {
                    string classValue = classAttribute.Value;

                    var fillPlaceholderMatches = FILL_PLACEHOLDER_REGEX.Matches(classValue);
                    foreach (var match in fillPlaceholderMatches.Cast<Match>())
                    {
                        int index = int.Parse(match.Groups[1].Value);
                        string replacementClassName = null;
                        if (fillCssClassNames.TryGetValue(index, out replacementClassName))
                        {
                            classValue = classValue.Replace(match.Value, replacementClassName);
                        }
                    }

                    var strokePlaceholderMatches = STROKE_PLACEHOLDER_REGEX.Matches(classValue);
                    foreach (var match in strokePlaceholderMatches.Cast<Match>())
                    {
                        int index = int.Parse(match.Groups[1].Value);
                        string replacementClassName = null;
                        if (strokeCssClassNames.TryGetValue(index, out replacementClassName))
                        {
                            classValue = classValue.Replace(match.Value, replacementClassName);
                        }
                    }

                    classAttribute.Value = classValue;
                }
            }

            result.Xml = shape;

            return result;
        }
    }
}
