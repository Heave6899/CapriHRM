using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace TestCapriHRM.Helper
{
    public class HRMAuthorization : ActionFilterAttribute, IAuthenticationFilter
    {
        public string permission { get; set; }
        HRMActionAuthenticationService hRMActionAuthenticationService = new HRMActionAuthenticationService();
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            int id = Convert.ToInt32(filterContext.HttpContext.Session["RoleId"].ToString());
            bool res = hRMActionAuthenticationService.HRMActionAuthenticationFillter(actionName, controllerName, id);
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserID"])) || res==false)
            {

                filterContext.Result = new HttpUnauthorizedResult();
            }
            
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                       { "controller","Account"},
                       { "action","Login"},

                   });
            }
        }
    }
}