using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrungTamAnhNgu.filters
{
    public class RoleFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            char role = ' ';
            if (filterContext.HttpContext.Request.Cookies.AllKeys.Contains("login"))
            {
                role = filterContext.HttpContext.Request.Cookies.Get("login")["token"].Last();
            } 
            string firstControler;
            switch (role)
            {
                case ' ':
                    firstControler = "Home";
                    break;
                case '1':
                    firstControler = "Admin";
                    break;
                case '2':
                    firstControler = "Teacher";
                    break;
                case '3':
                    firstControler = "Student";
                    break;
                default:
                    firstControler = "Home";
                    break;
            }
            
            string wantedController = (string)filterContext.RouteData.Values["controller"];
            if (!firstControler.Equals(wantedController))
            {
                System.Web.Routing.RouteValueDictionary routeValue = new System.Web.Routing.RouteValueDictionary();
                routeValue.Add("controller", firstControler);
                routeValue.Add("action", "Index");
                filterContext.Result = new RedirectToRouteResult(routeValue);
            }
        }
    }
}