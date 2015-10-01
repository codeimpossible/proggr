using System;
using System.Threading.Tasks;
using Worker.Models;

namespace Worker.Jobs
{
    public interface IJob
    {
        JobDescriptor JobDescriptor { get; }

        Task<JobResult> Run();
    }
}