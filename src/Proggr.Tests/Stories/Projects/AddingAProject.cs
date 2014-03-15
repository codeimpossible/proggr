using Proggr.Controllers;
using Proggr.Models;
using Simple.Data;
using Simple.Data.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proggr.Tests.Stories.Projects
{
    public class AddingAProject
    {
        private StubbedController<ProjectsController> _stub;
        public AddingAProject()
        {
            MockHelper.UseMockAdapter(new InMemoryAdapter());
            
            _stub = new StubbedController<ProjectsController>();

            // create a project
            var result = _stub.Controller.Create(new Project
            {
                id = 1,
                name = "TestProject",
                description = "a sample project",
                owner_id = Guid.NewGuid().ToString(),
                url = "https://github.com/codeimpossible/massiverecord"
            });
        }

        [Fact]
        public void API_ProjectsAreAvailableById()
        {
            var result = _stub.Controller.Details(1);

            dynamic jsonData = result.Data;

            Assert.Equal("TestProject", (string)jsonData.Data.name);
        }
    }
}
