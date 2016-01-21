using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class TemplateShape : Shape
    {
        private readonly string _clipPathMarkupId;
        private readonly Func<string, string> _markupFragmentAcessor;
        private readonly Template _template = null;
        private Dictionary<string, Design> _designs = new Dictionary<string, Design>();

        public TemplateShape(Template template, string clipPathMarkupId, Func<string, string> _markupFragmentAccessor)
        {
            _template = template;
        }

        public string ClipPathMarkup
        {
            get
            {
                return _markupFragmentAcessor(_clipPathMarkupId);
            }
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

        public override RenderDesignResult Render(Palette palette)
        {
            var result = new RenderDesignResult();
            result.Width = this.Width;
            result.Height = this.Height;

            var renderer = new TemplateRenderer(_template);
            foreach (var kvp in _designs)
            {
                renderer.AddDesign(kvp.Key, kvp.Value.Shape, kvp.Value.Palette);
            }
            var renderedDesign = renderer.Render();

            var defs = renderedDesign.Element("defs");
            if (defs != null)
            {
                foreach (var def in defs.Elements())
                {
                    result.Defs.Add(def);
                }
                defs.Remove();
            }

            result.ClipPath = this.ClipPathMarkup;

            // Remove the rendered design from it's svg element and just 
            // wrap it in a g element.
            var g = new XElement("g");
            g.Add(renderedDesign.Elements());
            result.Xml = g;

            return result;
        }
    }
}
