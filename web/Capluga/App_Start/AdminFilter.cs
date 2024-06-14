using System.Web.Mvc;
using System.Web.Routing;

namespace Capluga.App_Start
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Rol"] != null && filterContext.HttpContext.Session["Rol"].ToString() == "1")
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Error" }
                });
            }
        }
    }
}

