using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Moq;
using Newtonsoft.Json;
using Proggr.Data;
using Proggr.Data.Models;
using Should;
using Simple.Data;
using Simple.Data.Mocking;
using WebApp.Areas.Api.Controllers;
using WebApp.Areas.Api.Models;
using WebApp.Services;
using WebApp.Tests.Fixtures;
using Xunit;

namespace WebApp.Tests.Areas.Api
{
    public class ReposControllerTests
    {
        private readonly dynamic _database;
        private readonly ControllerHarness<ReposController> _harness;
        private readonly Mock<IGithubApiDataCacheService> _githubCache = new Mock<IGithubApiDataCacheService>(MockBehavior.Strict);

        public ReposControllerTests()
        {
            MockHelper.UseMockAdapter(new InMemoryAdapter());
            _database = Database.Open();

            _harness = new ControllerHarness<ReposController>(new ReposController(_githubCache.Object));
        }

        public class TheCreateAction : ReposControllerTests
        {
            [Fact]
            public void ShouldInsertARepoInTheDatabase()
            {
                var newRepo = CodeLocations.FakeNoId();
                var result = _harness.Controller.Create(newRepo) as JsonResult;
                var json = result.DeserializeData<CodeLocation>();

                var repos = (List<GithubApiRepository>)_database.CodeLocations.All();

                json.FullName.ShouldEqual(newRepo.FullName);
                json.IsPublic.ShouldEqual(newRepo.IsPublic);
                json.Name.ShouldEqual(newRepo.Name);
                repos.Count.ShouldEqual(1);
            }

            [Fact]
            public void WhenIdIsGiven_ShouldReturnAnError()
            {
                var newRepo = CodeLocations.Fake();
                var result = _harness.Controller.Create(newRepo) as JsonResult;

                var json = result.DeserializeData<PreconditionFailedException>();

                var repos = (List<GithubApiRepository>)_database.CodeLocations.All();

                _harness.Controller.Response.StatusCode.ShouldEqual((int)HttpStatusCode.PreconditionFailed);

                repos.Count.ShouldEqual(0);
                json.Message.ShouldEqual("Id cannot have a value when creating a CodeLocation");
            }
        }

        public class TheIndexAction : ReposControllerTests
        {
            [Fact]
            public void ShouldReturnRepositories_ThatMatchCodeLocations()
            {
                var locations = FixturesHelper.StoreFakes((location) => _database.CodeLocations.Insert(location), 10, CodeLocations.Fake)
                    .Take(5)
                    .Select(l => new GithubApiRepository()
                    {
                        CloneUrl = "https://github.com/" + l.FullName,
                        FullName = l.FullName,
                        Name = l.Name
                    }).ToList();
                _githubCache.Setup(gh => gh.GetApiData<List<GithubApiRepository>>(UserInformation.UserName_Jared, ApiStorageConstants.APIDATA_KEY_REPOSITORIES))
                            .Returns(locations);

                _harness.SetUserName(UserInformation.UserName_Jared);

                var result = _harness.Controller.Index() as JsonResult;

                var json = result.DeserializeData<List<CodeLocation>>();

                json.Count.ShouldEqual(locations.Count);
            }
        }
    }
}
