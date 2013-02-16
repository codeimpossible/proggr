using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proggr.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {

            ViewBag.client_id = _configuration.OAuthClientKey;

            ViewBag.current_user = _ticketHelper.GetUserFromCookie();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
