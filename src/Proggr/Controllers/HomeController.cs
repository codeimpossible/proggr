using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proggr.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.client_id = ConfigurationManager.AppSettings[ "github.oauth.clientkey" ];

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
