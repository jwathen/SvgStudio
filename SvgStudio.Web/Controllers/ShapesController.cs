using SvgStudio.Web.ViewModels.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using SvgStudio.Shared.StorageModel;
using AutoMapper;
using SvgStudio.Web.ViewModels.Shapes;

namespace SvgStudio.Web.Controllers
{
    [RoutePrefix("Shapes")]
    public partial class ShapesController : SvgStudioControllerBase
    {
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
                await model.SaveAsync(true);
            }
            return RedirectToAction(MVC.Shapes.Index());
        }
    }
}