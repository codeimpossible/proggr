using System;
using Simple.Data;
using Worker.Jobs;
using Worker.Models;

namespace Worker.Repositories
{
    public interface IJobRepository
    {
        JobDescriptor GetCurrentJob(Guid workerId);
        JobDescriptor GetNextJob(Guid workerId);
        JobDescriptor GetJob(Guid jobId);
        JobDescriptor UpdateJobStatus(JobDescriptor job, string newStatus);

        void ScheduleJob<TJob>(object arguments = null) 
            where TJob : IJob;

        void CompleteJob(Guid jobId, Guid workerId, object completedArgs);
    }
}