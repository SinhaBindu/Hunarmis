using Hunarmis;
using System.Web;
using System.Web.Mvc;

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var users = MvcApplication.CUser.UserId;
        if ( HttpContext.Current.Session["CUser"] == null || (string.IsNullOrWhiteSpace(users)))
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Account" },
                    { "action", "Login" }
                });
        }

        base.OnActionExecuting(filterContext);
    }
}
