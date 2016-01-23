using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class RenderDesignResult : IDefProvider
    {
        public DefinitionCollection Defs { get; set; }

        public RenderDesignResult()
        {
            Defs = new DefinitionCollection();
        }

        public XElement Xml { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public DefinitionCollection GetDefs()
        {
            return Defs;
        }

        public XDocument AsStandaloneSvg(double width, double height)
        {
            XDocument doc = new XDocument();
            var svg = new XElement(xmlns.svg + "svg");
            svg.Add(
                new XAttribute("viewBox", string.Format("0 0 {0} {1}", this.Width, this.Height)),
                new XAttribute("width", width),
                new XAttribute("height", height),
                new XAttribute("version", "1.1"));
            var defs = new XElement("defs");
            defs.Add(Defs);
            var g = new XElement("g");
            g.Add(Xml);
            svg.Add(defs);
            svg.Add(g);
            doc.Add(svg);
            return doc;
        }
    }
}
