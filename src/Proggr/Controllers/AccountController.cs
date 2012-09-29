using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Proggr.Models;
using Proggr.OAuth;
using WebMatrix.WebData;

namespace Proggr.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult LogOff()
        {
            OAuthTicketHelper.RemoveUserCookie();

            return RedirectToAction( "Index", "Home" );
        }        
    }
}
