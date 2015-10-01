using System;
using System.Timers;
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
        // TODO: refactor this out to another object?
        private static readonly System.Timers.Timer Timer = new System.Timers.Timer(new TimeSpan(0, 0, 0, 30).TotalMilliseconds);

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

            if (_worker.CurrentJob != Guid.Empty)
            {
                // start working on the job
                _presenter.CurrentJob = JobFactory.CreateJob(_jobRepository.GetCurrentJob(_worker.Id), _worker);
            }

            Timer.Elapsed += Tick;
            Tick(null, null);

            Application.Run((WorkloadView)_presenter.View);
        }

        private static void Tick(object sender, ElapsedEventArgs e)
        {
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
        }
    }
}
