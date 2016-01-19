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

        public XElement Render()
        {
            DefinitionCollection defs = new DefinitionCollection();
            List<XElement> groups = new List<XElement>();

            foreach (var designRegion in _template.DesignRegions.OrderBy(x => x.SortOrder))
            {
                Design design = null;
                if (_designs.TryGetValue(designRegion.Name, out design))
                {
                    var renderedDesign = design.Render();

                    var group = new XElement("g");
                    group.Add(new XAttribute("id", designRegion.Name));
                    string transform = string.Format("{0} {1}", CalculateTranslateTransform(designRegion), CalculateScaleTransform(designRegion, renderedDesign));
                    group.Add(new XAttribute("transform", transform));

                    if (!string.IsNullOrWhiteSpace(renderedDesign.ClipPath))
                    {
                        string clipPathId = string.Format("{0}_ClipPath", designRegion.Name);
                        var clipPathDef = new XElement("clipPath");
                        clipPathDef.Add(new XAttribute("id", clipPathId));
                        clipPathDef.Add(XElement.Parse(renderedDesign.ClipPath));
                        group.Add(new XAttribute("clip-path", string.Format("url(#{0})", clipPathId)));
                        defs.Add(clipPathDef);
                    }

                    group.Add(renderedDesign.Xml);
                    groups.Add(group);

                    defs.Add(renderedDesign.Defs);
                }
            }

            
            var defsElement = new XElement("defs");
            defsElement.Add(defs.ToList());

            XDocument doc = new XDocument();
            doc.Add(new XElement("svg"));
            doc.Root.Add(new XAttribute("version", "1.1"));
            if (defs.Count > 0)
            {
                doc.Root.Add(defsElement);
            }
            doc.Root.Add(groups);

            return doc.Root;
        }

        private string CalculateTranslateTransform(DesignRegion designRegion)
        {
            return string.Format("translate({0},{1})", designRegion.X, designRegion.Y);
        }

        private string CalculateScaleTransform(DesignRegion designRegion, RenderDesignResult design)
        {
            double scaleX = 0;
            if (design.Width > 0)
            {
                scaleX = ((double)designRegion.Width / design.Width);
            }

            double scaleY = 0;
            if (design.Height > 0)
            {
                scaleY = ((double)designRegion.Height / design.Height);
            }

            return string.Format("scale({0},{1})", scaleX, scaleY);
        }
    }
}
