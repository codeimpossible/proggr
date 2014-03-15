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
    public class ProjectsController : ControllerBase
    {
        [HttpPost]
        [MustBeLoggedIn]
        public JsonResult Create(Project newProject)
        {
            var db = OpenDatabaseConnection();

            var savedProject = db.Projects.Insert(newProject);

            Response.StatusCode = 201;

            return new JsonResponse(Response.StatusCode, savedProject);
        }

        [HttpGet]
        public JsonResult Details(int id)
        {
            var db = OpenDatabaseConnection();

            var data = db.Projects.Find(db.Projects.id == id);

            Response.StatusCode = 200;

            return new JsonResponse(Response.StatusCode, data);
        }
    }
}
