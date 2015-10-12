using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Repositories;

namespace Worker
{
    internal static class StartupIoC
    {
        internal static void RegisterIoC(IRegistry registry)
        {
            registry.Register<ICodeLocationRepository>(new CodeLocationRepository());
            registry.Register<IApiDataRepository>(new ApiDataRepository());
            registry.Register<IJobRepository>(new JobRepository());
            registry.Register<IWorkerRepository>(new WorkerRepository());
            registry.Register<IFileRepository>(new FileRepository());
        }
    }
}
