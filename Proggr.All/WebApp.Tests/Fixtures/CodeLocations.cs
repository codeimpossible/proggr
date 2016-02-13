using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Data.Models;

namespace WebApp.Tests.Fixtures
{
    public static class CodeLocations
    {
        public static CodeLocation FakeNoId()
        {
            var codeLocation = Fake();
            codeLocation.Id = Guid.Empty;
            return codeLocation;
        }

        public static CodeLocation Fake()
        {
            var projectName = Faker.Internet.UserName();
            var orgName = Faker.Internet.UserName();

            return new CodeLocation
            {
                Id = Guid.NewGuid(),
                FullName = orgName + "/" + projectName,
                Name = projectName,
                IsPublic = false
            };
        }
    }
}
