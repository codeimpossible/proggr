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

        private static readonly Locator _locator = new Locator();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartupIoC.RegisterIoC(_locator);

            // make sure we're able to do work
            _worker = new WorkerRegistrationController(_locator).EnsureWorkerRegistration();
            _jobRepository = _locator.Locate<IJobRepository>();
            _presenter = new WorkloadPresenter(new WorkloadView());

            var poller = new JobPoller(_presenter, _locator, _worker);

            if (_worker.CurrentJob != Guid.Empty)
            {
                // start working on the job
                _presenter.CurrentJob = JobFactory.CreateJob(_jobRepository.GetCurrentJob(_worker.Id), _worker, _locator);
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
