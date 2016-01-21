using SvgStudio.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SvgStudio.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FluentValidation.Mvc.FluentValidationModelValidatorProvider.Configure();
            Database.SetInitializer<SvgStudioDataContext>(null);
        }

        protected void Application_BeginRequest()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SvgStudio"].ConnectionString;
            Context.Items["SvgStudioDataContext.Current"] = new SvgStudioDataContext(connectionString);
        }

        protected void Application_EndRequest()
        {
            var db = (SvgStudioDataContext)Context.Items["SvgStudioDataContext.Current"];
            db?.Dispose();
        }
    }
}
