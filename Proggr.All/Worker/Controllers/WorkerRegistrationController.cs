using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Models;
using Worker.Repositories;

namespace Worker.Controllers
{
    public class WorkerRegistrationController
    {
        private readonly IWorkerRepository _repository;
        private WorkerState _workerState;

        public WorkerRegistrationController(IWorkerRepository repository)
        {
            _repository = repository;
        }

        public WorkerState EnsureWorkerRegistration()
        {
            if (_workerState == null)
            {
                object objLock;
                lock (objLock = new object())
                {
                    if (_workerState == null)
                    {
                        var worker = _repository.GetWorker(Environment.MachineName);
                        if (worker == null)
                        {
                            worker = _repository.RegisterWorker(Environment.MachineName);
                        }
                        _workerState = worker;
                    }
                }
            }
            return _workerState;
        }
    }
}
