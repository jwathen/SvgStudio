﻿using SvgStudio.Web.ViewModels.Licenses;
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
using SvgStudio.Shared.Helpers;
using SvgStudio.Shared.Drawing;

namespace SvgStudio.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    [RoutePrefix("Shapes")]
    public partial class ShapesController : SvgStudioControllerBase
    {
        private static object locker = new object();

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
                lock(locker)
                {
                    var factory = new DrawingFactory(db);
                    var drawingShape = new Shared.Drawing.BasicShape(
                        double.Parse(width),
                        double.Parse(height),
                        null,
                        (x) => xml);
                    drawingShape.Name = "Preview Shape " + paletteId;
                    var palette = factory.BuildPalette(paletteId);
                    var renderResult = drawingShape.Render(palette, "Preview");
                    var svgDocument = renderResult.AsStandaloneSvg(double.Parse(width), double.Parse(height));

                    return Content(XmlHelper.RenderDocument(svgDocument, false), "text/html");
                }
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
            xml = XmlHelper.RemoveRootSvgElement(xml);

            try
            {
                var parsed = XElement.Parse(xml);
            }
            catch
            {
                xml = "<g xmlns=\"" + xmlns.svg + "\" xmlns:xlink=\"" + xmlns.xlink + "\">" + xml + "</g>";
            }

            try
            {
                XElement svg = XElement.Parse(xml);
                string result = XmlHelper.WriteXElement(svg);
                return Content(result);
            }
            catch (Exception ex)
            {
            }
            return Content(string.Empty);
        }
    }
}