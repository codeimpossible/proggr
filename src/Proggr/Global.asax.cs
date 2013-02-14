﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
        private TicketHelper _ticketHelper = new OAuthTicketHelper();
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
            var user = _ticketHelper.GetUserFromCookie();
            var admin_users = ConfigurationManager.AppSettings["admin_users"];

            var can_perform_admin_task = Request.IsLocal || admin_users.Contains( user.Login );

            if ( can_perform_admin_task )
            {
                MiniProfiler.Start();

                // enable glimpse
                Request.Cookies.Add( new HttpCookie( "glimpseState", "on" ) { HttpOnly = false } );
            }

            if( Request.Url.AbsolutePath.ToLowerInvariant().EndsWith( "/elmah" ) && !can_perform_admin_task )
            {
                HttpContext.Current.Response.RedirectToRoute( new { controllers = "Errors", action = "NotFound" } );
            }
        }

        protected void Application_OnAuthenticateRequest()
        {
            var currentUser = HttpContext.Current.User;

            if( currentUser == null || !currentUser.Identity.IsAuthenticated )
            {
                var user = _ticketHelper.GetUserFromCookie();

                if( user != null )
                {
                    _ticketHelper.SetAuthCookie( user, true );
                }
            }
        }


        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}