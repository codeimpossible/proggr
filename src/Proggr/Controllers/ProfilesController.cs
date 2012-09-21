using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Models;

namespace Proggr.Controllers
{
    public class ProfilesController : Controller
    {

        private Users _usersTable = new Users();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details( string id )
        {
            var user = _usersTable.Single( where: "login = @0", args: id );

            if( user == null )
            {
                return RedirectToAction( "UserNotFound", new { controller = "Errors" } );
            }
            return View( user );
        }
    }
}
