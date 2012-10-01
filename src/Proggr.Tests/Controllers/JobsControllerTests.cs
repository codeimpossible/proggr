using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Controllers;
using Proggr.Controllers.Responses;
using Simple.Data;
using Simple.Data.Mocking;
using Xunit;

namespace Proggr.Tests.Controllers
{
    public class JobsControllerTests
    {
        JobsController _controller = new JobsController();

        Guid _test_worker_guid = new Guid( "{D38928E8-CE84-4B2A-BDA4-7A2D34FB2E53}" );

        public JobsControllerTests()
        {
            var adapter = new InMemoryAdapter();
            
            adapter.SetAutoIncrementColumn( "Jobs", "id" );

            MockHelper.UseMockAdapter( adapter );

            AddFixturesToDb();
        }

        [Fact]
        public void Test_Next_ReturnsNewJobsOnly()
        {
            var result = _controller.Next( _test_worker_guid ) as JsonResponse;

            dynamic jsonData = result.Data;

            Assert.NotNull( jsonData.Data.Job );
            Assert.Equal( "UpdateUserRepos", jsonData.Data.Job.Type );
        }

        [Fact]
        public void Test_Next_ReturnsJobsOrderedByCreated_at()
        {
            var db = Database.Open();
            db.Jobs.Insert( type: "AwardAchievements", status: -1, created_at: DateTime.Now.Subtract( TimeSpan.FromMinutes( 100 ) ), worker_id: null );

            var result = _controller.Next( _test_worker_guid ) as JsonResponse;

            dynamic jsonData = result.Data;

            Assert.Equal( "AwardAchievements", jsonData.Data.Job.Type );
        }

        [Fact]
        public void Test_Next_AssignsWorkerToJob()
        {
            var result = _controller.Next( _test_worker_guid ) as JsonResponse;

            dynamic jsonData = result.Data;

            Assert.Equal( _test_worker_guid.ToString(), jsonData.Data.Job.worker_id );
        }

        [Fact]
        public void Test_Next_ChangesStatusToInProgress()
        {
            var result = _controller.Next( _test_worker_guid ) as JsonResponse;

            dynamic jsonData = result.Data;

            Assert.Equal( 0, jsonData.Data.Job.status );
        }


        private void AddFixturesToDb()
        {
            var db = Database.Open();
            db.Jobs.Insert( type: "UpdateUserRepos", status: -1, created_at: DateTime.Now, worker_id: null );
            db.Jobs.Insert( type: "SlocRepo", status: 1, created_at: DateTime.Now.Subtract( TimeSpan.FromMinutes( 100 ) ), worker_id: null );
        }
    }
}
