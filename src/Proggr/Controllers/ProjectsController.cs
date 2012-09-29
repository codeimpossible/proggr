using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Models;
using Simple.Data;

namespace Proggr.Controllers
{
    public class ProjectsController : DataControllerBase
    {
        [HttpPost]
        public JsonResult Create( NewProject newProject )
        {
            var db = OpenDatabaseConnection();

            db.Projects.Insert( newProject );

            return Json( new { Status = 200, Message = "Project Added Successfully!" } );
        }
    }

    public class NewProject
    {
        public string name { get; set; }
        public string url { get; set; }
        public string owner_id { get; set; }
        public string description { get; set; }
    }
}
