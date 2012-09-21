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

            if( User.Identity.IsAuthenticated )
            {
                // they don't need to see the homepage, send them to their profile
                return RedirectToAction( "Details", new { controller = "Profiles", id = User.Identity.Name } );
            }

            ViewBag.client_id = ConfigurationManager.AppSettings[ "github.oauth.clientkey" ];

            return View();
        }

    }
}
