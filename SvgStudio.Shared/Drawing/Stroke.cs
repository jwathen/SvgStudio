using SvgStudio.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class Stroke : DefObject
    {
        public override string CssClass
        {
            get
            {
                return string.Format("Stroke_{0}_{1}", StringHelper.StripNonAlphaNumericChars(Color.ToString()), Width);
            }
        }

        public Color Color { get; set; } = Color.Transparent;

        public int Width { get; set; } = 0;

        public int[] DashArray { get; set; } = null;

        public override IEnumerable<XElement> ToDefXml()
        {
            var styles = new Dictionary<string, string>();
            if (Color != null)
            {
                styles["stroke"] = Color.ToString();
            }
            if (Width >= 0)
            {
                styles["stroke-width"] = Width.ToString();
            }
            if (DashArray != null && DashArray.Any())
            {
                styles["stroke-dasharray"] = string.Join(",", DashArray);
            }

            XElement style = new XElement("style");
            string stylesString = string.Join(Environment.NewLine, styles.Select(x => string.Format("{0}: {1};", x.Key, x.Value)));
            style.Value = string.Format(".{0} {{ {1} }}", CssClass, stylesString);
            yield return style;
        }
    }
}
