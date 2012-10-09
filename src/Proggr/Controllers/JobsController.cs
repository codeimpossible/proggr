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

            JsonJob job = db.Jobs.FindAll( db.Jobs.status == (int)JobStatus.New ).OrderBy( db.Jobs.created_at ).FirstOrDefault();
            
            if( job != null )
            {
                job.worker_id = worker_id;
                job.status = (int)JobStatus.InProgress;

                db.Jobs.UpdateById( job );
            }

            return new JsonResponse( 200, new { 
                Message = "Project Added Successfully!",
                Job = job ?? JsonJob.Empty( worker_id )
            } );
        }

        public class JsonJob
        {
            public static JsonJob Empty( Guid worker_id )
            {
                return new JsonJob { created_at = DateTime.Now, worker_id = worker_id, status = -1, type = "EmptyJob", id = -1 };
            }
            public int id { get; set; }
            public Guid worker_id { get; set; }
            public string type { get; set; }
            public int status { get; set; }
            public DateTime created_at { get; set; }
        }

    }
}
