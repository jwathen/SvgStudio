using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SvgStudio.Web.ViewModels.Shared;

namespace SvgStudio.Web.Controllers
{
    [RoutePrefix("Templates")]
    public partial class TemplatesController : SvgStudioControllerBase
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.Nav = "Templates";
        }

        [HttpGet]
        [Route("")]
        public virtual async Task<ActionResult> Index()
        {
            var templates = await db.Templates
                .Include(x => x.DesignRegions)
                .Where(x => x.IsMaster)
                .ToListAsync();

            var model = new List<TemplateViewModel>();
            foreach (var template in templates)
            {
                model.Add(await TemplateViewModel.BuildAsync(template, 10));
            }
            return View(model);
        }

        [HttpGet]
        [Route("Add")]
        public virtual async Task<ActionResult> Add()
        {
            var model = await TemplateViewModel.BuildAsync(null, 10);
            return View(model);
        }

        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Add(TemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await model.SaveAsync();
                await db.SaveChangesAsync();
            }
            return RedirectToAction(MVC.Templates.Index());
        }

        [HttpGet]
        [Route("Edit")]
        public virtual async Task<ActionResult> Edit(string id)
        {
            var template = await db.Templates.Include(x => x.DesignRegions).FirstAsync(x => x.Id == id);
            var model = await TemplateViewModel.BuildAsync(template, 10);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await model.SaveAsync();
                await db.SaveChangesAsync();
            }
            return RedirectToAction(MVC.Templates.Index());
        }
    }
}