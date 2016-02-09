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
using System.IO;
using Should;

namespace Worker.Tests.Jobs
{    
    public class CloneJobTests
    {
        private readonly Guid TEST_WORKER_ID = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

        private readonly Locator _testingLocator = new Locator();

        private readonly Mock<IRepositoryController> _repositoryController = new Mock<IRepositoryController>(MockBehavior.Strict);
        private readonly Mock<ICodeLocationRepository> _codeLocationRepository = new Mock<ICodeLocationRepository>(MockBehavior.Strict);
        private readonly Mock<IJobRepository> _jobRepository = new Mock<IJobRepository>(MockBehavior.Strict);

        protected void RegisterIoC()
        {
            _testingLocator.Register<IRepositoryController>(_repositoryController.Object);
            _testingLocator.Register<ICodeLocationRepository>(_codeLocationRepository.Object);
            _testingLocator.Register<IJobRepository>(_jobRepository.Object);
        }

        public class WhenCompletedSuccessfully : CloneJobTests
        {
            private readonly JobDescriptor _jobDescription;

            public WhenCompletedSuccessfully()
            {
                var fixture = Fixtures.JobDescriptions.CloneJobs.Proggr_TestTesting();
                _jobDescription = fixture.JobDescription;

                // first thing the job will do is ask where the Code Location should be stored

                _codeLocationRepository.Setup(m => m.GetCodeLocationLocalPath(fixture.RepositoryFullName))
                    .Returns(fixture.CloneDirectoryPath)
                    .Verifiable();

                // then the job will attempt to clone the repository to the disk

                _repositoryController.Setup(m => m.Clone(fixture.RepositoryUrl, fixture.CloneDirectoryPath, It.IsAny<CloneOptions>()))
                    .ReturnsAsync(fixture.CloneDirectoryPath)
                    .Verifiable();

                // after this, it will create a db entry for the code location
                
                _codeLocationRepository.Setup(m => m.CreateCodeLocation(fixture.RepositoryFullName, fixture.JobArgs.RepoName, fixture.JobArgs.Username, false))
                    .Returns(Fixtures.CodeLocations.CodeLocationA)
                    .Verifiable();

                // next up, time to claim ownership of this repository

                _codeLocationRepository.Setup(m => m.RecordCodeLocationOnWorker(TEST_WORKER_ID, Fixtures.CodeLocations.CodeLocationA.Id))
                    .Returns(true)
                    .Verifiable();

                // next, the job should schedule an ImportHistoryJob

                _jobRepository.Setup(m => m.ScheduleJob<ImportHistoryJob>(Fixtures.CodeLocations.CodeLocationA))
                    .Verifiable();

                // then the final step is to mark the current job complete

                _jobRepository.Setup(m => m.CompleteJob(It.IsAny<Guid>(), TEST_WORKER_ID, It.IsAny<object>()))
                    .Verifiable();

                RegisterIoC();
            }

            [Fact]
            public async Task Should_AddTheCodeLocation_ToTheWorkerCodeLocationsTable()
            {
                var job = new CloneJob(_jobDescription, TEST_WORKER_ID, _testingLocator);

                var result = await job.Run();

                result.ShouldBeType<JobSuccessResult>();

                // verify the flow of the job execution
                _repositoryController.VerifyAll();
                _codeLocationRepository.VerifyAll();
                _jobRepository.VerifyAll();
            }
        }
    }
}
