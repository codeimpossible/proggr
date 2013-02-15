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
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult MustBeLoggedIn()
        {
            return View();
        }

        public ActionResult AuthError()
        {
            return View();
        }
    }
}
