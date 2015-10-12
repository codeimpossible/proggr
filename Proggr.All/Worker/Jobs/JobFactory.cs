using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Models;
using Worker.Repositories;

namespace Worker.Jobs
{
    public class JobFactory
    {
        public static IJob CreateJob(JobDescriptor description, WorkerState worker, ILocator locator)
        {
            var jobType = Type.GetType(description.JobType);
            if (jobType == null)
            {
                throw new TypeAccessException("Could not create a type from " + description.JobType);
            }
            var instance = Activator.CreateInstance(jobType, description, worker.Id, locator) as IJob;

            return instance;
        }
    }
}
