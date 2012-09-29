using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Controllers;
using Simple.Data;
using Simple.Data.Mocking;
using Xunit;

namespace Proggr.Tests.Controllers
{
    public class ProjectsControllerTests
    {
        ProjectsController _controller = new ProjectsController();

        public ProjectsControllerTests()
        {
            MockHelper.UseMockAdapter( new InMemoryAdapter() );
        }

        [Fact]
        public void Test_Create_ReturnsAJSONResponse()
        {
            var result = _controller.Create( new NewProject
            {
                name = "TestProject",
                description = "a sample project",
                owner_id = Guid.NewGuid().ToString(),
                url = "https://github.com/codeimpossible/massiverecord"
            } );

            dynamic jsonData = result.Data;

            Assert.Equal( 200, (int)jsonData.Status );
            Assert.Equal( "Project Added Successfully!", (string)jsonData.Message );
        }

        [Fact]
        public void Test_Create_AddsRecordToDataStore()
        {
            var result = _controller.Create( new NewProject
            {
                name = "TestProject",
                description = "a sample project",
                owner_id = Guid.NewGuid().ToString(),
                url = "https://github.com/codeimpossible/massiverecord"
            } );

            var db = Database.Open();

            var numberOfProjects = db.Projects.All().Count();

            Assert.Equal( 1, numberOfProjects );
        }
    }
}
