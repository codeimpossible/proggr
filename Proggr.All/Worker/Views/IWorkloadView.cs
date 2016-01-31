using System;
using Worker.Jobs;
using Worker.Presenters;

namespace Worker.Views
{
    public interface IWorkloadView
    {
        IJob CurrentJob { get; set; }

        string Status { get; set; }

        WorkloadPresenter Presenter { set; }
    }
}
