using System;
using Worker.Models;

namespace Worker.Repositories
{
    public interface IWorkerRepository
    {
        WorkerState RegisterWorker(string machineName);

        WorkerState GetWorker(string machineName);

        WorkerState GetWorker(Guid workerId);
    }
}