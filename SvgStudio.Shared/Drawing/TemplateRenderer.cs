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

        public void AddDesign(string designRegionName, Shape shape, Palette palette)
        {
            if (shape == null)
            {
                _designs[designRegionName] = null;
            }
            else
            {
                var design = new Design
                {
                    Shape = shape,
                    Palette = palette
                };
                _designs[designRegionName] = design;
            }
        }

        public Design GetDesign(string designRegionName)
        {
            Design design = null;
            if (_designs.TryGetValue(designRegionName, out design))
            {
                return design;
            }
            else
            {
                return null;
            }
        }

        public XElement Render(string namingContext = "")
        {
            DefinitionCollection defs = new DefinitionCollection();
            List<XElement> groups = new List<XElement>();

            foreach (var designRegion in _template.DesignRegions.OrderBy(x => x.SortOrder))
            {
                Design design = GetDesign(designRegion.Name);
                if (design != null)
                {
                    var renderedDesign = design.Render(namingContext);

                    var group = new XElement("g");
                    group.Add(new XAttribute("id", designRegion.Name));
                    string transform = CalculateTransforms(designRegion, renderedDesign);
                    group.Add(new XAttribute("transform", transform));
                    group.Add(renderedDesign.Xml);
                    groups.Add(group);

                    defs.Add(renderedDesign.Defs);
                }
                groups.Add(XElement.Parse(designRegion.BuildPlaceholderXml()));
            }

            
            var defsElement = new XElement("defs");
            defsElement.Add(defs.ToList());

            XDocument doc = new XDocument();
            doc.Add(new XElement("svg"));
            doc.Root.Add(new XAttribute("version", "1.1"));
            doc.Root.Add(new XAttribute("width", _template.CalculateWidth()));
            doc.Root.Add(new XAttribute("height", _template.CalculateHeight()));
            if (defs.Count > 0)
            {
                doc.Root.Add(defsElement);
            }
            doc.Root.Add(groups);

            return doc.Root;
        }

        private string CalculateTransforms(DesignRegion designRegion, RenderDesignResult design)
        {
            //return string.Empty;
            double scale = Math.Min(designRegion.Width / design.Width, designRegion.Height / design.Height);
            double cx = designRegion.X + (designRegion.Width / 2);
            double cy = designRegion.Y + (designRegion.Height / 2);

            double translateX = cx - ((design.Width * scale) / 2);
            double translateY = cy - ((design.Height * scale) / 2);

            return string.Format("translate({0},{1}) scale({2},{2})", translateX, translateY, scale);
        }
    }
}
