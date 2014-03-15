using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Controllers;
using Simple.Data;
using Simple.Data.Mocking;
using Xunit;
using Proggr.Models;

namespace Proggr.Tests.Controllers
{
    public class ProjectsControllerTests
    {
        private StubbedController<ProjectsController> _stubContainer;

        public ProjectsControllerTests()
        {
            MockHelper.UseMockAdapter( new InMemoryAdapter() );

            _stubContainer = new StubbedController<ProjectsController>();
        }

        [Fact]
        public void Test_Create_ReturnsAJSONResponse()
        {
            var result = _stubContainer.Controller.Create(new Project
            {
                name = "TestProject",
                description = "a sample project",
                owner_id = Guid.NewGuid().ToString(),
                url = "https://github.com/codeimpossible/massiverecord"
            });

            dynamic jsonData = result.Data;

            Assert.Equal("TestProject", (string)jsonData.Data.name);
        }

        [Fact]
        public void Test_Create_Returns201()
        {
            var result = _stubContainer.Controller.Create(new Project
            {
                name = "TestProject",
                description = "a sample project",
                owner_id = Guid.NewGuid().ToString(),
                url = "http://www.google.com"
            });

            dynamic jsonData = result.Data;


            _stubContainer.Response.VerifySet(r => r.StatusCode = 201);

            Assert.Equal(_stubContainer.Response.Object.StatusCode, (int)jsonData.Meta.Status);
        }

        [Fact]
        public void Test_Create_AddsRecordToDataStore()
        {
            var result = _stubContainer.Controller.Create( new Project
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
