using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class TemplateRenderer
    {
        private readonly Template _template = null;  
        private Dictionary<string, Design> _designs = new Dictionary<string, Design>();

        public TemplateRenderer(Template template)
        {
            _template = template;
        }

        public void AddDesign(string designRegionKey, Shape shape, Palette palette)
        {
            var design = new Design
            {
                Shape = shape,
                Palette = palette
            };
            _designs[designRegionKey] = design;
        }

        public string Render()
        {
            HashSet<DefObject> defs = new HashSet<DefObject>();
            List<XElement> groups = new List<XElement>();

            foreach (var designRegion in _template.DesignRegions)
            {
                Design design = null;
                if (_designs.TryGetValue(designRegion.Key, out design))
                {
                    var renderedShape = design.Render();

                    var group = new XElement("g");
                    group.Add(new XAttribute("id", designRegion.Key));
                    group.Add(new XAttribute("transform", string.Format("translate({0},{1})", designRegion.X, designRegion.Y)));

                    var svg = new XElement("svg");
                    svg.Add(new XAttribute("width", designRegion.Width));
                    svg.Add(new XAttribute("height", designRegion.Height));
                    svg.Add(new XAttribute("viewBox", string.Format("0 0 {0} {1}", renderedShape.Width, renderedShape.Height)));
                    svg.Add(new XAttribute("preserveAspectRatio", "none"));

                    svg.Add(renderedShape.Xml);
                    group.Add(svg);
                    groups.Add(group);

                    foreach (var def in renderedShape.Defs)
                    {
                        defs.Add(def);
                    }
                }
            }

            var defsElement = new XElement("defs");
            defsElement.Add(defs.Select(x => x.ToDefXml()));

            XDocument doc = new XDocument();
            doc.Add(new XElement("svg"));
            doc.Root.Add(new XAttribute("version", "1.1"));
            doc.Root.Add(defsElement);
            doc.Root.Add(groups);

            string result = doc.ToString();
            result = result.Replace("<svg ", "<svg xmlns=\"http://www.w3.org/2000/svg\" ");
            return result;
        }
    }
}
