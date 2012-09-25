using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Proggr.Controllers.Filters;
using Proggr.Models;
using Proggr.OAuth;
using WebMatrix.WebData;

namespace Proggr.Controllers
{
    [MustBeLoggedIn]
    public class AccountController : Controller
    {

        private Users _usersTable = new Users();

        [HttpGet]
        public ActionResult LogOff()
        {
            OAuthTicketHelper.RemoveUserCookie();

            return RedirectToAction( "Index", "Home" );
        }        
    }
}
