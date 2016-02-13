using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using Simple.Data;
using Simple.Data.Mocking;
using WebApp.Tests.Fixtures;
using Xunit;

namespace WebApp.Tests
{
    public class FixtureTests
    {
        private dynamic _database;

        public FixtureTests()
        {
            MockHelper.UseMockAdapter(new InMemoryAdapter());
            _database = Database.Open();
        }

        [Fact]
        public void FixturesShouldGetInserted()
        {
            FixturesHelper.StoreFakes((location) => _database.CodeLocations.Insert(location), 10, CodeLocations.Fake);
            var locations = _database.CodeLocations.All();
            Assert.Equal(10, locations.Count());
        }

        [Fact]
        public void FixturesShouldBeQueryable()
        {
            FixturesHelper.StoreFakes((location) => _database.CodeLocations.Insert(location), 10, CodeLocations.Fake);
            FixturesHelper.StoreFakes((location) =>
            {
                location.IsPublic = true;
                _database.CodeLocations.Insert(location);
            }, 3, CodeLocations.Fake);

            var privateLocations = _database.CodeLocations.FindAllBy(IsPublic: false);
            var publicLocations = _database.CodeLocations.FindAllBy(IsPublic: true);
            Assert.Equal(10, privateLocations.Count());
            Assert.Equal(3, publicLocations.Count());
        }


        [Fact]
        public void FixturesShouldBeQueryableUsingAnInQuery()
        {
            var privateModels = FixturesHelper.StoreFakes((location) => _database.CodeLocations.Insert(location), 10, CodeLocations.Fake);
            var publicModels = FixturesHelper.StoreFakes((location) =>
            {
                location.IsPublic = false;
                _database.CodeLocations.Insert(location);
            }, 3, CodeLocations.Fake);

            var publicNames = publicModels.Select(c => c.FullName).ToArray();

            var privateLocations = _database.CodeLocations.FindAllBy(IsPublic: false);
            var publicLocations = _database.CodeLocations.FindAll(_database.CodeLocations.FullName == publicNames || _database.CodeLocations.IsPublic == true);
            Assert.Equal(13, privateLocations.Count());
            Assert.Equal(3, publicLocations.Count());
        }
    }
}
