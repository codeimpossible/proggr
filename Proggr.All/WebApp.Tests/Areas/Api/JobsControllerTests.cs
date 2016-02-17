using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Proggr.Data.Models;
using Should;
using Simple.Data;
using Simple.Data.Mocking;
using WebApp.Areas.Api.Controllers;
using WebApp.Services;
using WebApp.Tests.Fixtures;
using Xunit;

namespace WebApp.Tests.Areas.Api
{
    public class JobsControllerTests
    {
        private readonly dynamic _database;
        private readonly ControllerHarness<JobsController> _harness;
        private readonly Mock<IGithubApiDataCacheService> _githubCache = new Mock<IGithubApiDataCacheService>(MockBehavior.Strict);

        public JobsControllerTests()
        {
            MockHelper.UseMockAdapter(new InMemoryAdapter());
            _database = Database.Open();

            _harness = new ControllerHarness<JobsController>(new JobsController());
        }

        public class TheIndexAction : JobsControllerTests
        {
            [Fact]
            public void ShouldReturnAListOfJobs()
            {
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 5, JobFixture.CloneJob);

                var result = _harness.Controller.Index() as JsonResult;

                var jobs = result.DeserializeData<List<Job>>();

                jobs.Count.ShouldEqual(5);
            }

            [Fact]
            public void ShouldNotReturnJobsThatAreCompleted()
            {
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 5, JobFixture.CloneJob);
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 4, () => JobFixture.CloneJob().Complete(10.Days().Ago()));

                var result = _harness.Controller.Index() as JsonResult;

                var jobs = result.DeserializeData<List<Job>>();

                jobs.Count.ShouldEqual(5);
            }

            [Fact]
            public void ShoudlAllowFilteringByType()
            {
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 5, JobFixture.CloneJob);
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 2, JobFixture.ImportJob);
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 6, JobFixture.DetectAndHashJob);

                var cases = new Dictionary<string, int>()
                {
                    {"Worker.Jobs.CloneJob", 5},
                    {"Worker.Jobs.ImportJob", 2},
                    {"Worker.Jobs.DetectAndHashJob", 6}
                };

                foreach (var pair in cases)
                {
                    var result = _harness.Controller.Index(type: pair.Key) as JsonResult;

                    var jobs = result.DeserializeData<List<Job>>();

                    jobs.Count.ShouldEqual(pair.Value, $"{pair.Key} should have returned {pair.Value} items");
                }
            }

            [Fact]
            public void ShouldSupportTake()
            {
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 5, JobFixture.CloneJob);
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 2, JobFixture.ImportJob);
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 6, JobFixture.DetectAndHashJob);

                var result = _harness.Controller.Index(limit: 5) as JsonResult;

                var jobs = result.DeserializeData<List<Job>>();

                jobs.Count.ShouldEqual(5);

            }

            [Fact]
            public void ShouldSupportOffset()
            {
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 10, JobFixture.CloneJob);
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 10, JobFixture.ImportJob);
                FixturesHelper.StoreFakes((job) => _database.Jobs.Insert(job), 10, JobFixture.DetectAndHashJob);

                var allJobsInDb = (List<Job>)_database.Jobs.All();

                var result = _harness.Controller.Index(limit: 10, offset: 10) as JsonResult;

                var jobs = result.DeserializeData<List<Job>>();

                jobs.Count.ShouldEqual(10);
                jobs.First().Id.ShouldEqual(allJobsInDb[10].Id);
            }
        }
    }
}