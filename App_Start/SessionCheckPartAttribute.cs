using System.Web;
using System.Web.Mvc;

public class SessionCheckPartAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (HttpContext.Current.Session["PartUserId"] == null || !(bool)HttpContext.Current.Session["PartUserId"])
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    { "controller", "ParticipantUser" },
                    { "action", "Login" }
                });
        }

        base.OnActionExecuting(filterContext);
    }
}
