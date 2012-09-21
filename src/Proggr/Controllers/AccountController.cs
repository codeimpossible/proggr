using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proggr.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction( "Index", new { controller = "Home" } );
        }
    }
}
