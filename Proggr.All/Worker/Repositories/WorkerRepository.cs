using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Proggr.Data;
using Simple.Data;
using Worker.Models;

namespace Worker.Repositories
{
    public class WorkerRepository : SimpleDataRepository, IWorkerRepository
    {
        public WorkerState RegisterWorker(string machineName)
        {
            var worker = (WorkerState)Database.Workers.Insert(Id: Guid.NewGuid(), MachineName: machineName, State: (int)WorkerStatus.Idle, IPv4: IpAddress.v4(), DateCreated: DateTime.UtcNow, DateUpdated: DateTime.UtcNow);
            return worker;
        }

        public WorkerState GetWorker(string machineName)
        {
            var worker = (WorkerState)Database.Workers.FindAllByMachineName(MachineName: machineName).FirstOrDefault();
            return worker;
        }

        public WorkerState GetWorker(Guid workerId)
        {
            var worker = (WorkerState)Database.Workers.Get(workerId);
            return worker;
        }
    }
}
