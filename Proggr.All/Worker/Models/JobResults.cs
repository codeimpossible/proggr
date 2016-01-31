using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Worker.Jobs;

namespace Worker.Models
{
    public class JobSuccessResult : JobResult
    {
        public JobSuccessResult(IJob job) : base(job)
        {
        }
    }

    public class JobFailureResult : JobResult
    {
        public JobFailureResult(Exception e, IJob job) : base (job)
        {
            Exception = e;
        }

        public Exception Exception { get; private set; }
    }

    public abstract class JobResult
    {
        public JobResult(IJob job)
        {
            JobId = job.JobDescriptor.Id;
        }

        public Guid JobId { get; private set; }
    }
}
