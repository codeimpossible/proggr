using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proggr.Controllers
{
    public class ErrorsController : ControllerBase
    {
        public ActionResult Index()
        {
            Response.StatusCode = 412;
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult MustBeLoggedIn()
        {
            Response.StatusCode = 401;
            return View();
        }

        public ActionResult AuthError()
        {
            Response.StatusCode = 401;
            return View();
        }
    }
}
