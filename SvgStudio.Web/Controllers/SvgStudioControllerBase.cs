using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SvgStudio.Web.Controllers
{
    public class SvgStudioControllerBase : Controller
    {
        protected readonly SvgStudioDataContext db = null;

        public SvgStudioControllerBase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SvgStudio"].ConnectionString;
            db = new SvgStudioDataContext(connectionString);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            db?.Dispose();
        }
    }
}