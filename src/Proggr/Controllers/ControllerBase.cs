using Proggr.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proggr.Controllers
{
    public class ControllerBase : Controller
    {

    }

    public class AuthControllerBase: ControllerBase
    {
        protected TicketHelper _ticketHelper;

        public AuthControllerBase() : this(null)
        {

        }

        public AuthControllerBase(TicketHelper ticketHelper)
        {
            _ticketHelper = ticketHelper ?? new OAuthTicketHelper();
        }
    }
}
