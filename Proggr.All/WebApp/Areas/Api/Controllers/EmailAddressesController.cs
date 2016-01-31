using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Areas.Api.Models;
using WebApp.Data;

namespace WebApp.Areas.Api.Controllers
{
    public class EmailAddressesController : Controller
    {
        // GET: Api/EmailAddresses
        public ActionResult Index()
        {
            var matches = (List<UserEmailAddressMatch>) Storage.Current().GetUnClaimedEmailAddresses();

            return Json(matches, JsonRequestBehavior.AllowGet);
        }
    }
}