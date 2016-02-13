using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace WebApp.Tests
{
    public class ControllerHarness<CONTROLLER>
        where CONTROLLER : Controller
    {
        private readonly Mock<IIdentity> _identity = new Mock<IIdentity>();
        private readonly Mock<IPrincipal> _principal = new Mock<IPrincipal>();
        private readonly Mock<HttpContextBase> _httpContext = new Mock<HttpContextBase>();
        private readonly Mock<ControllerContext> _controllerContext = new Mock<ControllerContext>();
        private readonly Mock<HttpResponseBase> _response = new Mock<HttpResponseBase>();
        private readonly Mock<HttpRequestBase> _request = new Mock<HttpRequestBase>();

        public ControllerHarness(CONTROLLER controller)
        {
            Controller = controller;

            _principal.Setup(p => p.Identity).Returns(_identity.Object);
            _httpContext.Setup(h => h.User).Returns(_principal.Object);
            _httpContext.Setup(http => http.Request).Returns(_request.Object);
            _httpContext.Setup(http => http.Response).Returns(_response.Object);
            _controllerContext.Setup(c => c.HttpContext).Returns(_httpContext.Object);

            _request.SetupAllProperties();
            _response.SetupAllProperties();
            
            Controller.ControllerContext = _controllerContext.Object;
        }

        public void SetUserName(string username)
        {
            _identity.Setup(i => i.Name).Returns(username);
        }

        public CONTROLLER Controller { get; private set; }
    }
}
