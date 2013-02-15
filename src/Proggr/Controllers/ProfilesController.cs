using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Models;
using Simple.Data;

namespace Proggr.Controllers
{
    public class ProfilesController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details( string id )
        {

            var db = OpenDatabaseConnection();

            var user = db.Users.Find( db.Users.login == id );

            if( user == null )
            {
                return RedirectToAction( "UserNotFound", new { controller = "Errors" } );
            }
            return View( user );
        }
    }
}
