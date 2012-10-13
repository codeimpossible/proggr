using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Proggr.OAuth;
using Xunit;
using RestSharp;

namespace Proggr.Tests.OAuth
{
    public class GithubApiClientTests
    {

        IGithubApiClient _client;

        GithubProfile _defaultProfile = new GithubProfile();

        [Fact]
        public void Test_Constructor_SetsBaseUrl()
        {
            var mockRestSharpApiClient = new Mock<IRestClient>( MockBehavior.Strict );
            mockRestSharpApiClient
                .SetupSet( p => p.BaseUrl = It.IsAny<string>() )
                    .Callback<string>( value =>
                    {
                        Assert.Equal( "http://api.github.com", value );
                    } )
                    .Verifiable();
            _client = new GithubApiClient( mockRestSharpApiClient.Object );

            mockRestSharpApiClient.VerifyAll();
        }

        [Fact]
        public void Test_GetProfile_SendsRequestToGithub() 
        {
            var mockRestSharpApiClient = new Mock<IRestClient>( MockBehavior.Strict );
            mockRestSharpApiClient.SetupSet( p => p.BaseUrl = It.IsAny<string>() );
            mockRestSharpApiClient
                .Setup( m => m.Execute<GithubProfile>( It.IsAny<IRestRequest>() ) )
                    .Callback( (IRestRequest r) => {
                        Assert.Equal( "/user?access_token=test", r.Resource );
                        Assert.Equal( Method.GET, r.Method );
                    } )
                    .Returns<RestResponse<GithubProfile>>( null );
            
            _client = new GithubApiClient( mockRestSharpApiClient.Object );

            _client.GetProfile( "test" );

            mockRestSharpApiClient.VerifyAll();
        }

        [Fact]
        public void Test_GetProfile_GithubRequestFails_EmptyProfileIsReturned()
        {
            var mockRestSharpApiClient = new Mock<IRestClient>( MockBehavior.Strict );
            mockRestSharpApiClient.SetupSet( p => p.BaseUrl = It.IsAny<string>() );
            mockRestSharpApiClient
                .Setup( m => m.Execute<GithubProfile>( It.IsAny<IRestRequest>() ) )
                    .Returns<RestResponse<GithubProfile>>( null );

            _client = new GithubApiClient( mockRestSharpApiClient.Object );

            var result = _client.GetProfile( "test" );

            mockRestSharpApiClient.VerifyAll();
            Assert.IsType<EmptyGithubProfile>( result );
        }

        [Fact]
        public void Test_GetProfile_GithubRequestSucceeds_GithubProfileIsReturned()
        {
            var mockRestResponse = new Mock<IRestResponse<GithubProfile>>( MockBehavior.Strict );
            var mockRestSharpApiClient = new Mock<IRestClient>( MockBehavior.Strict );
            
            mockRestResponse.SetupGet( m => m.Data ).Returns( _defaultProfile );
            
            mockRestSharpApiClient.SetupSet( p => p.BaseUrl = It.IsAny<string>() );
            mockRestSharpApiClient
                .Setup( m => m.Execute<GithubProfile>( It.IsAny<IRestRequest>() ) )
                    .Returns( mockRestResponse.Object );

            _client = new GithubApiClient( mockRestSharpApiClient.Object );

            var result = _client.GetProfile( "test" );

            mockRestSharpApiClient.VerifyAll();
            Assert.IsType<GithubProfile>( result );
            Assert.NotNull( result );
        }
    }
}
