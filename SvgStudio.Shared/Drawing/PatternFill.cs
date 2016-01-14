using SvgStudio.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class PatternFill : Fill
    {
        public string Name { get; set; }

        public Design Design { get; set; }

        public override string CssClass
        {
            get
            {
                return string.Format("PatternFill_{0}", StringHelper.StripNonAlphaNumericChars(Name));
            }
        }

        public override IEnumerable<XElement> ToDefXml()
        {
            throw new NotImplementedException();
        }
    }
}
