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
        public ActionResult Index(string type = null, int limit = 10, int offset = 0)
        {
            var db = Storage.CreateConnection();

            var completedExpr = (db.Jobs.DateCompleted == null ||
                                 db.Jobs.DateCompleted >= DateTime.UtcNow.AddHours(-1.0));

            var typeExpr = db.Jobs.JobType != null;
            if (!String.IsNullOrEmpty(type))
            {
                typeExpr = db.Jobs.JobType == type;
            }

            // get all jobs that have not finished, or have finished in the last hour
            List<Job> jobs = db.Jobs.FindAll(typeExpr && completedExpr).Skip(offset).Take(limit);

            return Json(jobs, JsonRequestBehavior.AllowGet);
        }
    }
}