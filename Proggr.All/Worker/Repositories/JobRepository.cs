using System;
using Newtonsoft.Json;
using Proggr.Data;
using Worker.Jobs;
using Worker.Models;

namespace Worker.Repositories
{
    public class JobRepository : SimpleDataRepository, IJobRepository
    {
        public JobDescriptor GetCurrentJob(Guid workerId)
        {
            var jobId = Database.Workers.Get(workerId).CurrentJob;
            return Database.Jobs.Get(jobId);
        }

        public JobDescriptor GetNextJob(Guid workerId)
        {
            Database.GetNextJob(workerId);
            return GetCurrentJob(workerId);
        }

        public JobDescriptor GetJob(Guid jobId)
        {
            return Database.Jobs.Get(jobId);
        }

        public JobDescriptor UpdateJobStatus(JobDescriptor job, string newStatus)
        {
            Database.Jobs.UpdateById(Id: job.Id, Status: newStatus);
            job.Status = newStatus;
            return job;
        }

        public void ScheduleJob<TJob>(object arguments = null) where TJob : IJob
        {
            Database.Jobs.Insert(JobType: typeof(TJob).FullName, State: "New", Arguments: JsonConvert.SerializeObject(arguments));
        }

        public void CompleteJob(Guid jobId, Guid workerId, object completedArgs)
        {
            Database.Jobs.UpdateById(Id: jobId, State: "Completed", CompletedBy: workerId, DateCompleted: DateTime.UtcNow,
                CompletedArguments: JsonConvert.SerializeObject(completedArgs));

            // TODO: not sure if this is a good practice
            Database.Workers.UpdateById(Id: workerId, CurrentJob: null);
        }
    }
}
