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
    public class AccountController : ControllerBase
    {
        public AccountController(TicketHelper ticketHelper = null) : base(null, ticketHelper)
        {
            _ticketHelper = ticketHelper ?? _ticketHelper;
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            _ticketHelper.RemoveUserCookie();

            return RedirectToAction("Index", new { controller = "Home" });
        }
    }
}
