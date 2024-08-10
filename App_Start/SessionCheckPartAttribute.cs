using DocumentFormat.OpenXml.EMMA;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

public class SessionCheckPartAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (HttpContext.Current.Session["PartUserId"] == null)
        {
            var d = "OnActionExecuting : " + JsonConvert.SerializeObject(HttpContext.Current.Session["PartUserId"]);
            System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("~/logLoginUser.txt"), $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}: {d}{Environment.NewLine}");
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                    { "controller", "ParticipantUser" },
                    { "action", "Login" }
                });
        }

        base.OnActionExecuting(filterContext);
    }
}
