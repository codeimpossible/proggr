using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Proggr.Data.Models;
using WebApp.Data;

namespace WebApp.Areas.Api.Controllers
{
    public class JobsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var db = Storage.CreateConnection();

            // get all jobs that have not finished, or have finished in the last hour
            List<Job> jobs = db.Jobs.FindAll(db.Jobs.DateCompleted == null || db.Jobs.DateCompleted < DateTime.UtcNow.AddHours(-1.0));
            return Json(jobs, JsonRequestBehavior.AllowGet);
        }
    }
}