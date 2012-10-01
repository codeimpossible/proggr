using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Controllers.Responses;
using Simple.Data;

namespace Proggr.Controllers.Filters
{
    public class MustBeValidWorker : ActionFilterAttribute
    {
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            var worker_id = filterContext.ActionParameters[ "worker_id" ];

            var db = Database.OpenConnection( ConfigurationManager.AppSettings[ "SQLSERVER_CONNECTION_STRING" ] );

            var worker = db.Workers.Find( worker_id );

            if( worker == null )
            {
                filterContext.Result = new Http403Response();
            }
        }
    }
}