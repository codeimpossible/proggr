using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Controllers.Filters;
using Proggr.Controllers.Responses;
using Proggr.Models;
using Simple.Data;

namespace Proggr.Controllers
{
    public class ProjectsController : DataControllerBase
    {
        [HttpPost]
        [MustBeLoggedIn]
        public JsonResult Create( NewProject newProject )
        {
            // TODO: validate worker guid

            var db = OpenDatabaseConnection();

            db.Projects.Insert( newProject );

            return new JsonResponse( 200, new { Message = "Project Added Successfully!" } );
        }
    }

    public class NewProject
    {
        public string name { get; set; }
        public string url { get; set; }
        public string owner_id { get; set; }
        public string description { get; set; }
    }

    public class ProjectLookup
    {
        public string owner { get; set; }
        public string name { get; set; }
    }
}
