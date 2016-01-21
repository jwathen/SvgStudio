using System.Configuration;

namespace SvgStudio.Web
{
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static class AppSettings
    {
        public static string BootswatchTheme
        {
            get { return ConfigurationManager.AppSettings["BootswatchTheme"]; }
        }

        public static string ClientValidationEnabled
        {
            get { return ConfigurationManager.AppSettings["ClientValidationEnabled"]; }
        }

        public static string CodeMirrorTheme
        {
            get { return ConfigurationManager.AppSettings["CodeMirrorTheme"]; }
        }

        public static class Elmah
        {
            public static class Mvc
            {
                public static string AllowedRoles
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.allowedRoles"]; }
                }

                public static string AllowedUsers
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.allowedUsers"]; }
                }

                public static string DisableHandleErrorFilter
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.disableHandleErrorFilter"]; }
                }

                public static string DisableHandler
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.disableHandler"]; }
                }

                public static string IgnoreDefaultRoute
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.IgnoreDefaultRoute"]; }
                }

                public static string RequiresAuthentication
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.requiresAuthentication"]; }
                }

                public static string Route
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.route"]; }
                }

                public static string UserAuthCaseSensitive
                {
                    get { return ConfigurationManager.AppSettings["elmah.mvc.UserAuthCaseSensitive"]; }
                }
            }
        }

        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        public static string PreviewPaletteId
        {
            get { return ConfigurationManager.AppSettings["PreviewPaletteId"]; }
        }

        public static string UnobtrusiveJavaScriptEnabled
        {
            get { return ConfigurationManager.AppSettings["UnobtrusiveJavaScriptEnabled"]; }
        }

        public static string VecteezyPassword
        {
            get { return ConfigurationManager.AppSettings["VecteezyPassword"]; }
        }

        public static class Webpages
        {
            public static string Enabled
            {
                get { return ConfigurationManager.AppSettings["webpages:Enabled"]; }
            }

            public static string Version
            {
                get { return ConfigurationManager.AppSettings["webpages:Version"]; }
            }
        }
    }
}

