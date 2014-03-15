using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proggr.Controllers.Filters;
using Proggr.Controllers.Responses;
using Proggr.Models;

namespace Proggr.Controllers
{
    public class JobsController : ControllerBase
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
            var db = OpenDatabaseConnection();

            Job job = db.Jobs.FindAll( db.Jobs.status == (int)JobStatus.New ).OrderBy( db.Jobs.created_at ).FirstOrDefault();
            
            if( job != null )
            {
                job.worker_id = worker_id;
                job.status = (int)JobStatus.InProgress;

                db.Jobs.UpdateById( job );
            }

            return new JsonResponse( 200, new { 
                Job = job ?? Job.Empty( worker_id )
            } );
        }

    }
}
