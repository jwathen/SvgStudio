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
                var palette = factory.BuildPalette(paletteId);
                var renderResult = drawingShape.Render(palette);

                var svg = new XElement("svg",
                    new XAttribute("width", renderResult.Width),
                    new XAttribute("height", renderResult.Height),
                    new XAttribute("version", "1.1"),
                    new XAttribute("class", "center-block svg-content"));
                var defs = new XElement("defs");
                defs.Add(renderResult.Defs);
                var g = new XElement("g");
                g.Add(renderResult.Xml);
                svg.Add(defs);
                svg.Add(g);

                return Content(svg.ToString(), "text/html");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message, "text/html");
            }
        }
    }
}