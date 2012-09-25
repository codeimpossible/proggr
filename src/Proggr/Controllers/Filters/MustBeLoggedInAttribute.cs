using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Proggr.Models;

namespace Proggr.Controllers.Filters
{
    public class MustBeLoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            var user = OAuth.OAuthTicketHelper.GetUserFromCookie();

            if( user == WebsiteUser.Guest || user == null )
            {
                var routeValues = new RouteValueDictionary
                {
                    { "controller", "Errors" },
                    { "action", "MustBeLoggedIn" }
                };
                filterContext.Result = new RedirectToRouteResult( routeValues );
            }
        }
    }
}