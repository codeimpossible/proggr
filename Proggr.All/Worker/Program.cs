using System;
using System.Timers;
using System.Threading;
using System.Windows.Forms;
using Worker.Controllers;
using Worker.Jobs;
using Worker.Models;
using Worker.Presenters;
using Worker.Repositories;
using Worker.Views;

namespace Worker
{
    static class Program
    {
        private static WorkloadPresenter _presenter;
        private static IJobRepository _jobRepository;
        private static WorkerState _worker;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // make sure we're able to do work
            _worker = new WorkerRegistrationController(new WorkerRepository()).EnsureWorkerRegistration();
            _jobRepository = new JobRepository();
            _presenter = new WorkloadPresenter(new WorkloadView());

            var poller = new JobPoller(_presenter, _jobRepository, _worker);

            if (_worker.CurrentJob != Guid.Empty)
            {
                // start working on the job
                _presenter.CurrentJob = JobFactory.CreateJob(_jobRepository.GetCurrentJob(_worker.Id), _worker);
            }

            poller.Start();

            try
            {
                Application.Run((WorkloadView) _presenter.View);
            }
            finally
            {
                poller.Stop();
            }
        }
    }
}
