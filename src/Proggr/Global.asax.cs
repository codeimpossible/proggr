using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Proggr.Models;
using Proggr.OAuth;
using StackExchange.Profiling;

namespace Proggr
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            AuthConfig.RegisterAuth();
            WebApiConfig.Register( GlobalConfiguration.Configuration );
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
            RouteConfig.RegisterRoutes( RouteTable.Routes );
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            } 
        }

        protected void Application_OnAuthenticateRequest()
        {
            var currentUser = HttpContext.Current.User;

            if( currentUser == null || !currentUser.Identity.IsAuthenticated )
            {
                var user = OAuthTicketHelper.GetUserFromCookie();

                if( user != null )
                {
                    OAuthTicketHelper.SetAuthCookie( user, true );
                }
            }
        }

    }
}