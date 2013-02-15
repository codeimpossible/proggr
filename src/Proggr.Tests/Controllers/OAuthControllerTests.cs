using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proggr.Controllers;
using Xunit;
using Moq;
using Proggr.OAuth;
using System.Web.Mvc;
using Proggr.Configuration;
using Simple.Data.Mocking;
using Simple.Data;

namespace Proggr.Tests.Controllers {
    public class OAuthControllerTests {
        OauthController _controller;

        readonly Mock<IGithubApiClient> _mockGithubApi = new Mock<IGithubApiClient>( MockBehavior.Strict );
        readonly Mock<IGithubAuthClient> _mockGithubOauth = new Mock<IGithubAuthClient>( MockBehavior.Strict );
        readonly Mock<TicketHelper> _mockTicketHelper = new Mock<TicketHelper>( MockBehavior.Loose );

        public class FakeSettings : ConfigurationSettings
        {

            public string OAuthClientSecret {
                get { return "test_secret"; }
            }

            public string OAuthClientKey {
                get { return "test_key"; }
            }

            public string DatabaseConnectionString {
                get { return "connection_string"; }
            }
        }

        public OAuthControllerTests()
        {
            MockHelper.UseMockAdapter( new InMemoryAdapter() );
        }

        [Fact]
        public void Test_Callback_WithCode_AuthenticatesAgainstGithub() {
            MockGithubApi_ReturnProfile();
            MockGithubOauth_ReturnGoodResponse();

            _controller = new OauthController( 
                authClient: _mockGithubOauth.Object, 
                apiClient: _mockGithubApi.Object,
                ticketHelper: _mockTicketHelper.Object,
                settings: new FakeSettings() );

            var result = _controller.Callback( "test" );

            Assert.IsType<RedirectToRouteResult>( result );
        }

        private void MockGithubOauth_ReturnGoodResponse() {
            var response = new GithubOauthResponse() { StatusCode = 200, AccessToken = "test_token" };
            _mockGithubOauth.Setup( m => m.Authorize( "test_key", "test_secret", "test" ) )
                .Returns( response );
        }

        private void MockGithubApi_ReturnProfile() {
            var profile = new GithubProfile() { Login = "test_user" };
            _mockGithubApi.Setup( m => m.GetProfile( "test_token" ) ).Returns( profile );
        }
    }
}
