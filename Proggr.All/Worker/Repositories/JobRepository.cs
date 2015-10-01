using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Simple.Data;
using Worker.Jobs;
using Worker.Models;

namespace Worker.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly dynamic _database;

        public JobRepository()
        {
            _database = Database.OpenNamedConnection("DefaultConnection");
        }

        public JobDescriptor GetCurrentJob(Guid workerId)
        {
            var jobId = _database.Workers.Get(workerId).CurrentJob;
            return _database.Jobs.Get(jobId);
        }

        public JobDescriptor GetNextJob(Guid workerId)
        {
            _database.GetNextJob(workerId);
            return GetCurrentJob(workerId);
        }

        public JobDescriptor GetJob(Guid jobId)
        {
            return _database.Jobs.Get(jobId);
        }

        public JobDescriptor UpdateJobStatus(JobDescriptor job, string newStatus)
        {
            _database.Jobs.UpdateById(Id: job.Id, Status: newStatus);
            job.Status = newStatus;
            return job;
        }

        public void ScheduleJob<TJob>(object arguments = null) where TJob : IJob
        {
            _database.Jobs.Insert(JobType: typeof(TJob).FullName, State: "New", Arguments: JsonConvert.SerializeObject(arguments));
        }

        public void CompleteJob(Guid jobId, Guid workerId, object completedArgs)
        {
            _database.Jobs.UpdateById(Id: jobId, State: "Completed", CompletedBy: workerId, DateCompleted: DateTime.UtcNow,
                CompletedArguments: JsonConvert.SerializeObject(completedArgs));

            // TODO: not sure if this is a good practice
            _database.Workers.UpdateById(Id: workerId, CurrentJob: null);
        }
    }
}
