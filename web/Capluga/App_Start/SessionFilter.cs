using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

public class SessionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Session["Rol"].ToString() != "1")
        {
            base.OnActionExecuting(filterContext);
        }
        else
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                });
        }
    }
}
