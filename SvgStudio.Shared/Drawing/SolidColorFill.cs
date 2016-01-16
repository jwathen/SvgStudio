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

        public override string CssClass
        {
            get
            {
                return string.Format("SolidColorFill_{0}", StringHelper.StripNonAlphaNumericChars(Color.ToString()));
            }
        }

        public override IEnumerable<XElement> ToDefXml()
        {
            XElement style = new XElement("style");
            style.Value = string.Format(".{0} {{ fill: {1}; }}", CssClass, Color);
            yield return style;
        }

        public static SolidColorFill Transparent
        {
            get
            {
                return new SolidColorFill(Color.Transparent);
            }
        }

        public override string ToString()
        {
            return CssClass;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is SolidColorFill))
            {
                return false;
            }
            else
            {
                return obj.GetHashCode() == this.GetHashCode();
            }
        }
    }
}
