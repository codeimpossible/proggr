using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Areas.Api.Filters;

namespace WebApp.Areas.Api.Controllers
{
    public class JobsController : Controller
    {
        [Worker]
        public ActionResult Index()
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}