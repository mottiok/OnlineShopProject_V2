using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShopProject.Filters
{
    public class RejectUnauthorizedUsers : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.IsInRole("Admins"))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {{ "Controller", "Albums" },
                                      { "Action", "Index" } });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}