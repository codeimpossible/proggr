using System.Web.Mvc;

namespace WebApp.Areas.Api.Filters
{
    public class CurrentUserOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var username = (httpContext.Request.RequestContext.RouteData.Values["username"] as string) ?? (httpContext.Request["username"] as string);

            var currentUserName = httpContext.User.Identity.Name;

            if (currentUserName != username)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}