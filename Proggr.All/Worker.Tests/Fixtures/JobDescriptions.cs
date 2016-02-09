using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Worker.Jobs;
using Worker.Models;
using Xunit;

namespace Worker.Tests.Fixtures
{
    public static class JobDescriptions
    {
        public static class CloneJobs
        {
            public const string WORKING_DIRECTORY = "T:\\.worker";

            public class CloneJobTestFixture
            {
                public CloneJobArgs JobArgs { get; set; }
                public JobDescriptor JobDescription { get; set; }
                public string RepositoryUrl { get; set; }
                public string UserName { get; set; }
                public string RepositoryName { get; set; }
                public string CloneDirectoryPath { get; set; }
                public string RepositoryFullName { get; set; }
            }

            public static CloneJobTestFixture Proggr_TestTesting()
            {
                var args = new CloneJobArgs()
                {
                    Url = "http://github.com/proggr_test/testing.git",
                    Username = "proggr_test",
                    RepoName = "testing"
                };
                return new CloneJobTestFixture()
                {
                    RepositoryName = args.RepoName,
                    RepositoryUrl = args.Url,
                    UserName = args.Username,
                    CloneDirectoryPath = WORKING_DIRECTORY + "\\proggr_test-testing",
                    RepositoryFullName = "proggr_test/testing",
                    JobArgs = args,
                    JobDescription = new JobDescriptor()
                    {
                        Id = Guid.NewGuid(),
                        Status = "New",
                        Arguments = JsonConvert.SerializeObject(args),
                        JobType = "Worker.Jobs.CloneJob",
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow
                    }
                };
            }
        }
    }
}
