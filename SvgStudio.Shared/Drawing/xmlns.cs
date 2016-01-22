using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public static class xmlns
    {
        public static XNamespace svg = "http://www.w3.org/2000/svg";
        public static XNamespace xlink = "http://www.w3.org/1999/xlink";

        public static string xlinkUri = "http://www.w3.org/1999/xlink";
        public static XAttribute xlinkAttr = new XAttribute(XNamespace.Xmlns + "xlink", "http://www.w3.org/1999/xlink");
    }
}
