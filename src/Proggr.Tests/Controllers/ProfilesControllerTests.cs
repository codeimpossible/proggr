using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Proggr.Controllers;
using Simple.Data;
using Simple.Data.Mocking;
using Xunit;

namespace Proggr.Tests.Controllers
{
    public class ProfilesControllerTests
    {

        ProfilesController _controller = new ProfilesController();

        public ProfilesControllerTests()
        {
            MockHelper.UseMockAdapter( new InMemoryAdapter() );

            // create a valid user in the "database"
            var db = Database.Open();
            db.Users.Insert( id: 1, 
                login: "jared",
                avatar_url: "http://google.com", 
                created_at: DateTime.Now.ToUniversalTime(), 
                name: "Jared Barbozsa" );
        }

        [Fact]
        public void Test_Details_ValidUserId_ReturnsUserRecord()
        {
            var result = _controller.Details( "jared" ) as ViewResult;

            dynamic model = result.Model;

            Assert.Equal( "jared", model.login );
        }

        [Fact]
        public void Test_Details_NonExistentUserId_RedirectsToUserNotFound()
        {
            var result = _controller.Details( "susan" ) as RedirectToRouteResult;

            var route = result.RouteValues;

            Assert.NotNull( result );
            Assert.Equal( "Errors", route["controller"] );
            Assert.Equal( "UserNotFound", route[ "action" ] );
        }
    }
}
