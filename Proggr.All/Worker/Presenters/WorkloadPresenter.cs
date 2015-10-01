using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Worker.Controllers;
using Worker.Jobs;
using Worker.Models;
using Worker.Repositories;
using Worker.Views;

namespace Worker.Presenters
{
    public class WorkloadPresenter
    {
        private IJob _currentJob;

        public WorkloadPresenter(IWorkloadView view)
        {
            View = view;
            View.Presenter = this;

            DisplayJobStatus();
        }

        public IJob CurrentJob
        {
            get { return _currentJob; }
            set
            {
                _currentJob = value;
                DisplayJobStatus();
                if (_currentJob != null) 
                    RunJob();
                // TODO: listen to events from the new job
            }
        }

        public IWorkloadView View { get; private set; }

        private void DisplayJobStatus()
        {
            if (_currentJob != null)
            {
                View.CurrentJob = _currentJob;
                View.Status = _currentJob.JobDescriptor.Status;
            }
            else
            {
                View.Status = "No Jobs Assigned.";
                View.CurrentJob = null;
            }
        }

        private async void RunJob()
        {
            var result = await CurrentJob.Run();
            if (result is JobFailureResult)
            {
                CurrentJob.JobDescriptor.Status = "Failed";
                DisplayJobStatus();
            }

            CurrentJob = null;
        }
    }
}
