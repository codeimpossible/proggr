using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Data.Models;

namespace WebApp.Tests
{
    public static class JobExtensions
    {
        public static Job Complete(this Job job, DateTime completionDate)
        {
            job.DateCompleted = completionDate;
            return job;
        }
    }
}
