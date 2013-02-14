using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Controllers;
using Simple.Data;
using Simple.Data.Mocking;
using Xunit;
using System.Web.Mvc;
using Moq;
using Proggr.OAuth;

namespace Proggr.Tests.Controllers
{
    public class AccountControllerTests
    {
        AccountController _controller = new AccountController();

        private Mock<TicketHelper> _mockTicketHelper;

        public AccountControllerTests()
        {
            MockHelper.UseMockAdapter(new InMemoryAdapter());

            _mockTicketHelper = new Mock<TicketHelper>(MockBehavior.Strict);
        }

        [Fact]
        public void Test_Logout_RedirectsToHomeIndex()
        {
            _mockTicketHelper.Setup(m => m.RemoveUserCookie()).Verifiable();

            _controller = new AccountController(_mockTicketHelper.Object);

            var result = _controller.LogOff() as RedirectToRouteResult;

            var route = result.RouteValues;

            Assert.NotNull(result);
            Assert.Equal("Index", route["action"]);
            Assert.Equal("Home", route["controller"]);
        }
    }
}
