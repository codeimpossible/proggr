using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Models;

namespace Proggr.Controllers
{
    public class ProjectsController : Controller
    {

        private Projects _projectsTable = new Projects();

        [HttpPost]
        public JsonResult Create( NewProject newProject )
        {
            _projectsTable.Insert( newProject );

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
