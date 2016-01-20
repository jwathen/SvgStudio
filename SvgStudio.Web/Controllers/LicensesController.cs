using SvgStudio.Web.ViewModels.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using SvgStudio.Shared.StorageModel;

namespace SvgStudio.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    [RoutePrefix("Licenses")]
    public partial class LicensesController : SvgStudioControllerBase
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.Nav = "Licenses";
        }

        [HttpGet]
        [Route("")]
        public virtual async Task<ActionResult> Index()
        {
            var licenses = await db.Licenses.ToListAsync();
            var model = new List<LicenseViewModel>();
            foreach (var license in licenses)
            {
                model.Add(await LicenseViewModel.BuildAsync(license));
            }
            return View(model);
        }

        [HttpGet]
        [Route("Add")]
        public virtual async Task<ActionResult> Add()
        {
            var model = await LicenseViewModel.BuildAsync(null);
            return View(model);
        }

        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Add(LicenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await model.SaveAsync(true);
            }
            return RedirectToAction(MVC.Licenses.Index());
        }

        [HttpGet]
        [Route("Edit")]
        public virtual async Task<ActionResult> Edit(string id)
        {
            var license = await db.Licenses.FindAsync(id);
            var model = await LicenseViewModel.BuildAsync(license);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(LicenseViewModel model)
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
            return RedirectToAction(MVC.Licenses.Index());
        }
    }
}