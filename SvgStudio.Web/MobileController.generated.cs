// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace SvgStudio.Web.Controllers
{
    public partial class MobileController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public MobileController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected MobileController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<SvgStudio.Web.Web.JsonNetResult> Sync()
        {
            var callInfo = new T4MVC_SvgStudio_Web_Web_JsonNetResult(Area, Name, ActionNames.Sync);
            return System.Threading.Tasks.Task.FromResult(callInfo as SvgStudio.Web.Web.JsonNetResult);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public MobileController Actions { get { return MVC.Mobile; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Mobile";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Mobile";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string GetVersion = "GetVersion";
            public readonly string Sync = "Sync";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string GetVersion = "GetVersion";
            public const string Sync = "Sync";
        }


        static readonly ActionParamsClass_Sync s_params_Sync = new ActionParamsClass_Sync();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Sync SyncParams { get { return s_params_Sync; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Sync
        {
            public readonly string request = "request";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_MobileController : SvgStudio.Web.Controllers.MobileController
    {
        public T4MVC_MobileController() : base(Dummy.Instance) { }

        [NonAction]
        partial void GetVersionOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetVersion()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetVersion);
            GetVersionOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SyncOverride(T4MVC_SvgStudio_Web_Web_JsonNetResult callInfo, SvgStudio.Shared.ServiceContracts.Requests.MobileSyncRequest request);

        [NonAction]
        public override System.Threading.Tasks.Task<SvgStudio.Web.Web.JsonNetResult> Sync(SvgStudio.Shared.ServiceContracts.Requests.MobileSyncRequest request)
        {
            var callInfo = new T4MVC_SvgStudio_Web_Web_JsonNetResult(Area, Name, ActionNames.Sync);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "request", request);
            SyncOverride(callInfo, request);
            return System.Threading.Tasks.Task.FromResult(callInfo as SvgStudio.Web.Web.JsonNetResult);
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
