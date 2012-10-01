using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Controllers.Filters;
using Proggr.Controllers.Responses;

namespace Proggr.Controllers
{
    public class JobsController : DataControllerBase
    {

        private enum JobStatus
        {
            New = -1,
            InProgress = 0,
            Failed = 1
        }

        [HttpPost]
        [MustBeValidWorker]
        public JsonResult Next( Guid worker_id )
        {
            // TODO: validate worker guid

            var db = OpenDatabaseConnection();

            var job = db.Jobs.FindAll( db.Jobs.status == (int)JobStatus.New ).OrderBy( db.Jobs.created_at ).First();

            job.worker_id = worker_id.ToString();
            job.status = (int)JobStatus.InProgress;

            db.Jobs.UpdateById( job );

            return new JsonResponse( 200, new { 
                Message = "Project Added Successfully!",
                Job = job
            } );
        }

    }
}
