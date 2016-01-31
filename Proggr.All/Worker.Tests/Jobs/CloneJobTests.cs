using System;
using System.Threading.Tasks;
using LibGit2Sharp;
using Moq;
using Newtonsoft.Json;
using Worker.Controllers;
using Worker.Jobs;
using Worker.Models;
using Worker.Repositories;
using Xunit;

namespace Worker.Tests.Jobs
{
    
    public class CloneJobTests
    {
        private const string TEST_USERNAME = "proggr_test";
        private const string TEST_REPOSITORY_URL = "http://github.com/proggr_test/testing.git";
        private const string TEST_WORKING_DIRECTORY = "T:\\.worker";
        private Guid TEST_WORKER_ID = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

        private readonly Locator _testingLocator;

        private readonly Mock<IRepositoryController> _mockRepoController;
        private readonly Mock<ICodeLocationRepository> _mockCodeLocationRepo;

        public CloneJobTests()
        {
            _testingLocator = new Locator();
            _mockCodeLocationRepo = new Mock<ICodeLocationRepository>(MockBehavior.Strict);
            _mockRepoController = new Mock<IRepositoryController>(MockBehavior.Strict);
        }

        protected void RegisterIoC()
        {
            _testingLocator.Register<IRepositoryController>(_mockRepoController.Object);
            _testingLocator.Register<ICodeLocationRepository>(_mockCodeLocationRepo.Object);
        }

        public class WhenCompletedSuccessfully : CloneJobTests
        {
            private readonly JobDescriptor _jobDescription;

            public WhenCompletedSuccessfully()
            {
                _jobDescription = new JobDescriptor()
                {
                    Id = Guid.NewGuid(),
                    Status = "New",
                    Arguments = JsonConvert.SerializeObject(new CloneJobArgs()
                    {
                        Url = TEST_REPOSITORY_URL,
                        Username = TEST_USERNAME
                    }),
                    JobType = "Worker.Jobs.CloneJob",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                _mockRepoController.Setup(
                    m => m.Clone(ItShould.Be(TEST_REPOSITORY_URL), ItShould.Be(TEST_WORKING_DIRECTORY), It.IsAny<CloneOptions>()));

                // setup some mocks we can validate
                _mockCodeLocationRepo.Setup(m => m.CreateCodeLocation(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Verifiable();
                _mockCodeLocationRepo.Setup(m => m.RecordCodeLocationOnWorker(ItShould.Be(TEST_WORKER_ID), It.IsAny<Guid>())).Verifiable();

                RegisterIoC();
            }

            [Fact]
            public async Task Should_AddTheCodeLocation_ToTheWorkerCodeLocationsTable()
            {
                var job = new CloneJob(_jobDescription, TEST_WORKER_ID, _testingLocator);

                var result = await job.Run();

                // assertion
            }
        }
    }
}
