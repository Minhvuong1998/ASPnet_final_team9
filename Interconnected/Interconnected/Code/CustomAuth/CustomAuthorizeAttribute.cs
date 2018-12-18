using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interconnected.Code.CustomAuth
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (CurrentUser == null)
                return false;
            if (Role == null || Role == "")
            {
                return (CurrentUser == null) ? false : true;
            }
            else
            {
                return (CurrentUser.IsInRole(Role) || CurrentUser.ROLE.Equals(ConstanAppkey.ADMIN())) ? true : false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;
            if (CurrentUser == null)
            {
                routeData = new RedirectToRouteResult
                (new System.Web.Routing.RouteValueDictionary
                (new
                {
                    controller = "Login",
                    action = "Index",
                    ReturnUrl = filterContext.RequestContext.HttpContext.Request.Url.ToString()
                }
                ));
            }
            else
            {
                if (CurrentUser.IsInRole(Role))
                {
                    routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                        (new
                        {
                            controller = "Home",
                            action = "Index",
                        }
                        ));
                }
                else
                {
                    routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "Error",
                        action = "NotFound",
                        message = "Bạn không đủ quyền"
                    }
                    ));
                }
            }
            filterContext.Result = routeData;
        }  
    }
}