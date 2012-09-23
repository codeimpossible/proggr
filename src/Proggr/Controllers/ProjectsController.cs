using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Controllers.Responses;
using Proggr.Models;

namespace Proggr.Controllers
{
    public class ProjectsController : Controller
    {

        private Projects _projectsTable = new Projects();

        [HttpPost]
        public JsonResult Create( NewProject newProject, Guid workerid )
        {
            // TODO: validate worker guid

            _projectsTable.Insert( newProject );

            return new JsonResponse( 200, new { Message = "Project Added Successfully!" } );
        }

        [HttpPost]
        public JsonResult Details( ProjectLookup projectLookup, Guid workerid )
        {
            // TODO: validate worker guid

            var project = _projectsTable.Query(
                            @"
                            SELECT TOP 1 P.*
                            FROM projects P
                            INNER JOIN users U ON U.login = @1 AND U.id = P.user_id
                            WHERE P.name = @0
                            ", projectLookup.name, projectLookup.owner );

            return new JsonResponse( 200, project );
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
