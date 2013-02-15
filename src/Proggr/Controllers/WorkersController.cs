using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Controllers.Filters;
using Proggr.Models;

namespace Proggr.Controllers
{
    public class WorkersController : ControllerBase
    {

        [MustBeAdminUser]
        public ActionResult Index()
        {
            // TODO: list the recently active workers and which account they belong to
            return View();
        }

        [MustBeLoggedIn]
        public ActionResult Run()
        {
            // TODO: check for the workerid for the current user
            var db = OpenDatabaseConnection();

            var current_user = db.Users.Find( db.Users.login == User.Identity.Name );
            dynamic worker = db.Workers.Find( db.Workers.user_id == current_user.id );

            // TODO: if no worker exists, create one
            if( worker == null )
            {
                worker = db.Workers.Insert( new { user_id = current_user.id, last_report = DateTime.Now.ToUniversalTime() } );
            }

            // TODO: render the worker view, with the worker model

            return View( worker );
        }
    }
}
