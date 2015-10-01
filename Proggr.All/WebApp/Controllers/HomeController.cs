using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.CurrentUserName = User.Identity.Name;
            ViewBag.CurrentUserId = User.Identity.GetUserId();

            return View();
        }
    }
}
