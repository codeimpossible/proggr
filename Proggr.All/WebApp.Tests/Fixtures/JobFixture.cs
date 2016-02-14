using System;
using Proggr.Data.Models;

namespace WebApp.Tests.Fixtures
{
    public class JobFixture
    {
        private static readonly Random gen = new Random();

        public static Job CloneJob()
        {
            return Fake("Worker.Jobs.CloneJob");
        }

        public static Job ImportJob()
        {
            return Fake("Worker.Jobs.ImportJob");
        }

        public static Job DetectAndHashJob()
        {
            return Fake("Worker.Jobs.DetectAndHashJob");
        }

        private static Job Fake(string jobType)
        {
            var dateCreated = RandomDay(DateTime.Now);

            return new Job()
            {
                DateCreated = dateCreated,
                DateUpdated = dateCreated,
                DateCompleted = null,
                Id = Guid.NewGuid(),
                JobType = jobType
            };
        }

        private static DateTime RandomDay(DateTime max)
        {
            return RandomDay(max, new DateTime(1995, 1, 1));
        }
        private static DateTime RandomDay(DateTime max, DateTime min)
        {
            var range = (max - min).Days;
            return min.AddDays(gen.Next(range));
        }
    }
}