using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worker.Jobs;
using Worker.Models;
using Worker.Presenters;
using Worker.Repositories;

namespace Worker.Controllers
{
    public class JobPoller
    {
        private readonly int _interval = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
        private Thread _thread;
        private bool _stop;

        private readonly WorkloadPresenter _presenter;
        private readonly IJobRepository _jobRepository;
        private readonly WorkerState _worker;

        public JobPoller(WorkloadPresenter presenter, IJobRepository jobRepository, WorkerState worker, int? intervalSeconds = null)
        {
            _interval = intervalSeconds ?? _interval;

            _presenter = presenter;
            _jobRepository = jobRepository;
            _worker = worker;
        }

        public void Start()
        {
            if (_thread == null)
            {
                _thread = new Thread(Run);
                _thread.Start();
            }
            _stop = false;
        }

        private void Run()
        {
            while (!_stop)
            {
                // Wait the specified interval before checking again...
                if (_presenter.CurrentJob == null)
                {
                    var desc = _jobRepository.GetNextJob(_worker.Id);
                    if (desc != null)
                    {
                        _worker.CurrentJob = desc.Id;

                        // TODO: change this so that it happens automatically when CurrentJob is set on the workerstate
                        _presenter.CurrentJob = JobFactory.CreateJob(desc, _worker);
                    }
                }

                Thread.Sleep(_interval);
            }

            try
            {
                _thread.Abort();
            }
            catch (ThreadAbortException tae)
            {
            }
            finally
            {
                _thread = null;
            }
        }

        public void Stop()
        {
            _stop = true;
        }
    }
}
