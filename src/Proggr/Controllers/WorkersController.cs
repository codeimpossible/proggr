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
    public class WorkersController : Controller
    {
        private Workers _workersTable = new Workers();
        private Users _usersTable = new Users();

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
            var current_user = _usersTable.Single( where: "login = @0", args: User.Identity.Name );
            var worker = _workersTable.Single( where: "user_id = " + current_user.id );

            // TODO: if no worker exists, create one
            if( worker == null )
            {
                worker = _workersTable.Insert( new { user_id = current_user.id, last_report = DateTime.Now.ToUniversalTime() } );
            }

            // TODO: render the worker view, with the worker model

            return View( worker );
        }
    }
}
