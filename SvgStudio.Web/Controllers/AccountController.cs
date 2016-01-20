using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SvgStudio.Web.Controllers
{
    [RequireHttps]
    [RoutePrefix("Account")]
    public partial class AccountController : Controller
    {
        [HttpGet]
        [Route("SignIn")]
        public virtual ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("SignIn")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SignIn(string username, string password)
        {
            if (username == "admin" && password == AppSettings.Password)
            {
                FormsAuthentication.SetAuthCookie("admin", true);
                return RedirectToAction(MVC.Licenses.Index());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
                return View();
            }
        }

        [HttpGet]
        [Route("SignOut")]
        public virtual ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Account.SignIn());
        }
    }
}