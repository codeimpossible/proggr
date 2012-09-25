using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proggr.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult MustBeLoggedIn()
        {
            return View();
        }
    }
}
