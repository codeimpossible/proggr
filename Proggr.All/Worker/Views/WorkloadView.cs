using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Worker.Jobs;
using Worker.Presenters;
using Worker.Views;

namespace Worker.Views
{
    public partial class WorkloadView : Form, IWorkloadView
    {
        private IJob _currentJob;

        public WorkloadView()
        {
            InitializeComponent();

            statusLabel.Text = jobIdLabel.Text = String.Empty;
        }

        public IJob CurrentJob
        {
            get { return _currentJob; }
            set
            {
                _currentJob = value;
                jobIdLabel.Text = _currentJob?.JobDescriptor.Id.ToString("N") ?? String.Empty;
            }
        }

        public string Status
        {
            get { return statusLabel.Text; }
            set { statusLabel.Text = value; }
        }

        public WorkloadPresenter Presenter { private get; set; }
    }
}
