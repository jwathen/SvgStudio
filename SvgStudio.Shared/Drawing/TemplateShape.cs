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

        public TemplateShape(Template template, string clipPathMarkupId, Func<string, string> markupFragmentAccessor)
        {
            _template = template;
            _clipPathMarkupId = clipPathMarkupId;
            _markupFragmentAcessor = markupFragmentAccessor;
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

        public override RenderDesignResult Render(Palette palette, string namingContext)
        {
            var result = new RenderDesignResult();
            result.Width = this.Width;
            result.Height = this.Height;

            var renderer = new TemplateRenderer(_template);
            foreach (var kvp in _designs)
            {
                renderer.AddDesign(kvp.Key, kvp.Value.Shape, kvp.Value.Palette);
            }
            var renderedDesign = renderer.Render(namingContext);

            var defs = renderedDesign.Element("defs");
            if (defs != null)
            {
                foreach (var def in defs.Elements())
                {
                    result.Defs.Add(def);
                }
                defs.Remove();
            }

            var g = new XElement("g");
            string clipMatchMarkup = ClipPathMarkup;
            if (clipMatchMarkup != null)
            {
                string clipPathId = AddNamingPrefixToId("ClipPath", namingContext);
                var clipPathDef = new XElement("clipPath");
                clipPathDef.Add(new XAttribute("id", clipPathId));
                clipPathDef.Add(XElement.Parse(clipMatchMarkup));
                g.Add(new XAttribute("clip-path", string.Format("url(#{0})", clipPathId)));
                result.Defs.Add(clipPathDef);
            }
            g.Add(renderedDesign.Elements());
            result.Xml = g;


            return result;
        }
    }
}
