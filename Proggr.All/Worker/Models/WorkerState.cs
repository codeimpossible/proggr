using System;

namespace Worker.Models
{
    public enum WorkerStatus : int
    {
        Idle = 0,
        Busy = 1
    }

    public class WorkerState
    {
        public WorkerState()
        {

        }

        public Guid Id { get; set; }
        public string MachineName { get; set; }
        public int State { get; set; }
        public Guid CurrentJob { get; set; }

        public WorkerStatus WorkerStatus
        {
            get { return (WorkerStatus)State; }
            set { State = (int)value; }
        }

        public void CheckState()
        {
            if (WorkerStatus == WorkerStatus.Idle)
            {
                if (CurrentJob != Guid.Empty)
                {
                    // we've got a job and we're not doing anything...

                }
            }
        }
    }
}