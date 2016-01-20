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

namespace SvgStudio.Web.Controllers
{
    [RoutePrefix("Licenses")]
    public partial class LicensesController : SvgStudioControllerBase
    {
        [HttpGet]
        [Route("")]
        public virtual async Task<ActionResult> Index()
        {
            var licenses = await db.Licenses.ToListAsync();
            var model = Mapper.Map<List<LicenseViewModel>>(licenses);
            return View(model);
        }

        [HttpGet]
        [Route("Add")]
        public virtual ActionResult Add()
        {
            return View(new LicenseViewModel());
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
                var license = Mapper.Map<License>(model);
                license.Id = UniqueId.Generate();
                db.Licenses.Add(license);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(MVC.Licenses.Index());
        }

        [HttpGet]
        [Route("Edit")]
        public virtual async Task<ActionResult> Edit(string id)
        {
            var license = await db.Licenses.FindAsync(id);
            var model = Mapper.Map<LicenseViewModel>(license);
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
                var license = await db.Licenses.FindAsync(model.Id);
                Mapper.Map(model, license);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(MVC.Licenses.Index());
        }
    }
}