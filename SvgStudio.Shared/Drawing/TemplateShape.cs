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
            string outlineId = AddNamingPrefixToId("Outline", namingContext);
            if (clipMatchMarkup != null)
            {
                var outlineDef = XElement.Parse(clipMatchMarkup);
                outlineDef.Add(new XAttribute("id", outlineId));
                result.Defs.Add(outlineDef);

                string clipPathId = AddNamingPrefixToId("ClipPath", namingContext);
                var clipPathDef = new XElement("clipPath");
                clipPathDef.Add(new XAttribute("id", clipPathId));
                clipPathDef.Add(new XElement("use", new XAttribute(xmlns.xlink + "href", string.Format("#{0}", outlineId))));
                result.Defs.Add(clipPathDef);

                g.Add(new XAttribute("clip-path", string.Format("url(#{0})", clipPathId)));
            }
            g.Add(renderedDesign.Elements());
            g.Add(new XElement("use", new XAttribute(xmlns.xlink + "href", string.Format("#{0}", outlineId))));
            result.Xml = g;

            return result;
        }

        public override RenderDesignResult RenderPreview()
        {
            var palette = new Palette();

            // https://color.adobe.com/Black-Math-color-theme-183602/
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#EB5937")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#1C1919")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#403D3C")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#456F74")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#D3CBBD")));

            // https://color.adobe.com/Painted-peacock-color-theme-946978/
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#E5DD00")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#8CB202")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#008C74")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#004C66")));
            palette.Fills.Add(new SolidColorFill(Color.FromHex("#332B40")));

            int fillIndex = 0;
            foreach(var designRegion in _template.DesignRegions)
            {
                string markupFragment = string.Format("<rect width=\"{0}\" height=\"{1}\" data-fill-index=\"{2}\" />", designRegion.Width, designRegion.Height, fillIndex);
                BasicShape box = new BasicShape(designRegion.Width, designRegion.Height, null, x => markupFragment);
                AddDesign(designRegion.Name, box, palette);
                fillIndex++;
            }

            return Render(null, "Preview");
        }
    }
}
