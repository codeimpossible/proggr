using Moq;
using Proggr.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Proggr.Tests
{
    public class StubbedController<TController>
        where TController : Proggr.Controllers.ControllerBase, new() 
    {
        private TController _controller;

        public StubbedController()
        {
            if (_controller == null)
            {
                Context = new Mock<HttpContextBase>();
                Response = new Mock<HttpResponseBase>();
                ControllerContext = new Mock<ControllerContext>();

                Context.Setup(x => x.Response).Returns(Response.Object);
                ControllerContext.Setup(x => x.HttpContext).Returns(Context.Object);

                _controller = new TController { ControllerContext = ControllerContext.Object };
            }
        }

        public TController Controller { get { return _controller; } }

        public Mock<HttpContextBase> Context { get; set; }
        public Mock<HttpResponseBase> Response { get; set; }
        public Mock<ControllerContext> ControllerContext { get; set; }
    }
}
