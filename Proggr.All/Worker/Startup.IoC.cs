using Worker.Controllers;
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
            registry.Register<IRepositoryController>(new RepositoryController());
        }
    }
}
