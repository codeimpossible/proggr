using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Data;

namespace Proggr.Controllers
{
    public abstract class DataControllerBase : Controller
    {
        public dynamic OpenDatabaseConnection()
        {
            return Database.OpenConnection( ConfigurationManager.AppSettings[ "SQLSERVER_CONNECTION_STRING" ] );
        }
    }
}
