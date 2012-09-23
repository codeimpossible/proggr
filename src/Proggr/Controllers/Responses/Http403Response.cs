using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proggr.Controllers.Responses
{

    public class Http403Response : ActionResult
    {
        public override void ExecuteResult( ControllerContext context )
        {
            // Set the response code to 403.
            context.HttpContext.Response.StatusCode = 403;
        }
    }

}