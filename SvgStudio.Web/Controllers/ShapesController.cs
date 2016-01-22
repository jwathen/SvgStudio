using SvgStudio.Web.ViewModels.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using SvgStudio.Shared.StorageModel;
using SvgStudio.Web.ViewModels.Shapes;
using System.Xml.Linq;
using SvgStudio.Shared.Materializer;
using SvgStudio.Web.Helpers;

namespace SvgStudio.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    [RoutePrefix("Shapes")]
    public partial class ShapesController : SvgStudioControllerBase
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.Nav = "Shapes";
        }

        [HttpGet]
        [Route("")]
        public virtual async Task<ActionResult> Index()
        {
            var shapes = await db.Shapes.ToListAsync();
            List<ShapeViewModel> model = new List<ShapeViewModel>();
            foreach(var shape in shapes)
            {
                model.Add(await ShapeViewModel.BuildAsync(shape));
            }
            return View(model);
        }

        [HttpGet]
        [Route("Add")]
        public virtual async Task<ActionResult> Add()
        {
            var model = await ShapeViewModel.BuildAsync(null);
            return View(model);
        }

        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Add(ShapeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await model.SaveAsync(true);
            }
            return RedirectToAction(MVC.Shapes.Index());
        }

        [HttpGet]
        [Route("Edit")]
        public virtual async Task<ActionResult> Edit(string id)
        {
            var shape = await db.Shapes.FindAsync(id);
            var model = await ShapeViewModel.BuildAsync(shape);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(ShapeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                if (model.Action == "Delete")
                {
                    await model.DeleteAsync(true);
                }
                else if (model.Action == "Save")
                {
                    await model.SaveAsync(true);
                }
            }
            return RedirectToAction(MVC.Shapes.Index());
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("GeneratePreview")]
        public virtual ActionResult GeneratePreview(
            string width, 
            string height, 
            string xml, 
            string paletteId)
        {
            try
            {
                var factory = new DrawingFactory(db);
                var drawingShape = new Shared.Drawing.BasicShape(
                    double.Parse(width),
                    double.Parse(height), 
                    null,
                    (x) => xml);
                drawingShape.Name = "Preview Shape " + paletteId;
                var palette = factory.BuildPalette(paletteId);
                var renderResult = drawingShape.Render(palette);

                var svg = new XElement("svg",
                    new XAttribute("viewBox", string.Format("0 0 {0} {1}", renderResult.Width, renderResult.Height)),
                    new XAttribute("version", "1.1"),
                    new XAttribute("class", "center-block svg-content"),
                    new XAttribute(XNamespace.Xmlns + "xlink", "http://www.w3.org/1999/xlink"));
                var defs = new XElement("defs");
                defs.Add(renderResult.Defs);
                var g = new XElement("g");
                g.Add(renderResult.Xml);
                svg.Add(defs);
                svg.Add(g);

                var result = XmlHelper.AddRootNamespace(svg, "http://www.w3.org/2000/svg");

                return Content(result, "text/html");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message, "text/html");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("AutoFixShapeMarkup")]
        public virtual ActionResult AutoFixShapeMarkup(string xml)
        {
            XNamespace xmlns = "http://www.w3.org/2000/svg";
            try
            {
                var parsed = XElement.Parse(xml);
                // if the outer element is an svg element then remove it
                //if (parsed.Name == "svg" || parsed.Name == xmlns + "svg")
                //{
                //    xml = "<g>" + string.Join(Environment.NewLine, parsed.Elements()) + "</g>";
                //}
            }
            catch
            {
                xml = "<g>" + xml + "</g>";
            }

            if (xml.Contains("xlink:") && !xml.Contains("xmlns:xlink"))
            {
                xml = "<g xmlns:xlink=\"http://www.w3.org/1999/xlink\" > " + xml + "</g>";
            }

            try
            {
                XElement svg = XElement.Parse(xml);
                string result = XmlHelper.EmitStrokeAndFillAttributesFirst(svg);
                return Content(result);
            }
            catch (Exception ex)
            {
            }
            return Content(string.Empty);
        }
    }
}