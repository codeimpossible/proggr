using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Controllers.Responses;

namespace Proggr.Controllers.Filters
{
    public class MustBeAdminUserAttribute : ActionFilterAttribute
    {
        private string _adminUsers = ConfigurationManager.AppSettings[ "admin_users" ];

        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            var isAdminUser = _adminUsers.Split( ',' ).Contains( filterContext.HttpContext.User.Identity.Name );

            if( !isAdminUser )
            {
                filterContext.Result = new Http403Response();
            }
        }
    }
}