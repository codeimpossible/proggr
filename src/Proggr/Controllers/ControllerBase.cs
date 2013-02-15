using Proggr.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Configuration;
using Simple.Data;

namespace Proggr.Controllers
{
    public class ControllerBase : Controller
    {
        protected TicketHelper _ticketHelper;
        protected ConfigurationSettings _configuration;

        public ControllerBase() : this(null, null) { }
        public ControllerBase(ConfigurationSettings settings, TicketHelper ticketHelper)
        {
            _configuration = settings ?? new WebConfigurationSettings();
            _ticketHelper = ticketHelper ?? new OAuthTicketHelper();
        }

        public dynamic OpenDatabaseConnection() {
            return Database.OpenConnection( _configuration.DatabaseConnectionString );
        }
    }
}
