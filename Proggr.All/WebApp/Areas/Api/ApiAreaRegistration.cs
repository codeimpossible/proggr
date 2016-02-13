using System.Web.Mvc;

namespace WebApp.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Api";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "CurrentUserApi",
                url: "api/user/{action}",
                defaults: new { controller = "CurrentUser", action = "Index" }
            );

            context.MapRoute(
                name: "EmailAddressesApi",
                url: "api/emailaddresses/{action}",
                defaults: new { controller = "EmailAddresses", action = "Index" }
            );

            context.MapRoute(
                "Api_default",
                "api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}