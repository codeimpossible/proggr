using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Proggr.Models;
using Proggr.OAuth;

namespace Proggr.Controllers.Filters
{
    public class MustBeLoggedInAttribute : ActionFilterAttribute
    {
        private TicketHelper _ticketHelper = new OAuthTicketHelper();

        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            var user = _ticketHelper.GetUserFromCookie();

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