using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proggr.Models
{
    public class Job
    {
        public static Job Empty(Guid worker_id)
        {
            return new Job { created_at = DateTime.Now, worker_id = worker_id, status = -1, type = "EmptyJob", id = -1 };
        }
        public int id { get; set; }
        public Guid worker_id { get; set; }
        public string type { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
    }
}