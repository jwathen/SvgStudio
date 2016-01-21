using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SvgStudio.Web.Controllers
{
    public partial class HelpController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.Nav = "Help";
        }

        [Route("Help/Index")]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}