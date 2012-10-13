//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Proggr.Controllers;
//using Xunit;
//using Moq;
//using Proggr.OAuth;

//namespace Proggr.Tests.Controllers
//{
//    public class OAuthControllerTests
//    {
//        OauthController _controller = new OauthController();

//        Mock<IGithubAuthClient> _authClientMock;
//        Mock<IGithubApiClient> _apiClientMock;

//        public void Test_Callback_WithCodeAttemptsToAuthAgainstGithub()
//        {
//            _authClientMock = new Mock<IGithubAuthClient>( MockBehavior.Strict );
//            _apiClientMock = new Mock<IGithubApiClient>( MockBehavior.Loose );

//            _controller = new OauthController( _authClientMock.Object, _apiClientMock.Object );

//            _controller.Callback( "response_code" );

            
//        }
//    }
//}
