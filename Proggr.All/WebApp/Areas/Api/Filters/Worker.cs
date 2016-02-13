using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.Areas.Api.Filters
{
    public class Worker : ActionFilterAttribute
    {
        private const string workerUserAgent = "";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var useragent = httpContext.Request.UserAgent;
            var workerId = httpContext.Request.Headers["X-Proggr-Worker-Id"];

            var db = Storage.CreateConnection();
            var worker = db.Workers.Get(workerId);

            if (useragent != workerUserAgent || String.IsNullOrWhiteSpace(workerId) || worker == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}