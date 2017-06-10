using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AJSoftWeb.Models;
using AJSoftEntity;
using static AJSoftEntity.Classes.EnumData;
using System.Web.Security;
using System.Web.Routing;

namespace AJSoftWeb.Classes
{
    public class BaseController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            base.Initialize(requestContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null)
                return;

            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request == null)
                return;

            var ControllerName = filterContext.Controller.GetType().Name.ToLower();
            var ActionName = filterContext.ActionDescriptor.ActionName.ToLower();
            if (HttpContext.User == null || HttpContext.User.Identity.IsAuthenticated == false ||
                (((User)Session[En_UserSession.User.ToString()]).RoleId != (int)En_Role.Primary
                ))
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
                return;
            }


            filterContext.HttpContext.Response.ClearHeaders();
            filterContext.HttpContext.Response.AppendHeader("Refresh", string.Concat(Session.Timeout * 60, ";Url=", "/Account/Login"));

            base.OnActionExecuting(filterContext);
        }
    }
}