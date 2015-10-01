using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Worker.Models;
using Worker.Repositories;

namespace Worker.Jobs
{
    public abstract class JobArgs
    {
        
    }

    public abstract class Job : IJob
    {
        protected readonly IJobRepository _jobRepository;
        protected readonly IApiDataRepository _apiRepository;
        protected readonly JobDescriptor _jobDescription;

        public Job(JobDescriptor jobDescription, Guid workerId, IJobRepository jobRepository = null, IApiDataRepository apiDataRepository = null)
        {
            _jobRepository = jobRepository ?? new JobRepository();
            _apiRepository = apiDataRepository ?? new ApiDataRepository();

            Id = jobDescription.Id;
            WorkerId = workerId;
            JobDescriptor = jobDescription;
        }
        
        public Guid Id { get; set; }
        public string Status { get; set; }
        public Guid WorkerId { get; set; }

        public JobDescriptor JobDescriptor { get; private set; }

        public virtual async Task<JobResult> Run()
        {
            throw new NotImplementedException();
        }
    }
}
