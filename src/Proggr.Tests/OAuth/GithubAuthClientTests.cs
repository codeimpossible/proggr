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
    public class GithubAuthClientTests
    {
        IGithubAuthClient _client;

        Mock<IRestClient> _mockRestClient;

        BadAuthResponse _badResponse = new BadAuthResponse();
        GoodAuthResponse _goodResponse = new GoodAuthResponse();

        public GithubAuthClientTests()
        {
            _mockRestClient = new Mock<IRestClient>( MockBehavior.Strict );
        }

        [Fact]
        public void Test_Constructor_SetsBaseUrl()
        {
            _mockRestClient
                .SetupSet( p => p.BaseUrl = It.IsAny<string>() )
                    .Callback<string>( value =>
                    {
                        Assert.Equal( "https://github.com/", value );
                    } )
                    .Verifiable();
            _client = new GithubAuthorizationClient( _mockRestClient.Object );

            _mockRestClient.VerifyAll();
        }

        [Fact]
        public void Test_Authorize_BadResponseReturnsAuthError()
        {
            _mockRestClient.SetupSet( p => p.BaseUrl = It.IsAny<string>() );
            _mockRestClient.Setup( m => m.Execute<GithubOauthResponse>( It.IsAny<RestRequest>() ) )
                .Returns( _badResponse );

            _client = new GithubAuthorizationClient( _mockRestClient.Object );

            var result = _client.Authorize( "a", "b", "c" );

            Assert.Equal( true, result.IsError );
        }

        [Fact]
        public void Test_Authorize_AddsParamsToRequest()
        {
            _mockRestClient.SetupSet( p => p.BaseUrl = It.IsAny<string>() );
            _mockRestClient.Setup( m => m.Execute<GithubOauthResponse>( It.IsAny<RestRequest>() ) )
                .Callback( ( IRestRequest r ) => {
                    var client_id = r.Parameters.Where( p => p.Name == "client_id" ).First();
                    var client_secret = r.Parameters.Where( p => p.Name == "client_secret" ).First();
                    var code = r.Parameters.Where( p => p.Name == "code" ).First();

                    Assert.Equal( "clientid", client_id.Value );
                    Assert.Equal( "secret", client_secret.Value );
                    Assert.Equal( "token", code.Value );

                    Assert.Equal( "login/oauth/access_token", r.Resource );
                    Assert.Equal( Method.POST, r.Method );
                })
                .Returns( _badResponse );

            _client = new GithubAuthorizationClient( _mockRestClient.Object );

            var result = _client.Authorize( "clientid", "secret", "token" );
        }

        [Fact]
        public void Test_Authorize_GoodResponseReturnsAuthResponse()
        {
            _mockRestClient.SetupSet( p => p.BaseUrl = It.IsAny<string>() );
            _mockRestClient.Setup( m => m.Execute<GithubOauthResponse>( It.IsAny<RestRequest>() ) )
                .Returns( _goodResponse );

            _client = new GithubAuthorizationClient( _mockRestClient.Object );

            var result = _client.Authorize( "a", "b", "c" );

            Assert.Equal( false, result.IsError );
        }

        [Fact]
        public void Test_Authorize_GoodResponseSetsStatusCodeIntoResponse()
        {
            _mockRestClient.SetupSet( p => p.BaseUrl = It.IsAny<string>() );
            _mockRestClient.Setup( m => m.Execute<GithubOauthResponse>( It.IsAny<RestRequest>() ) )
                .Returns( _goodResponse );

            _client = new GithubAuthorizationClient( _mockRestClient.Object );

            var result = _client.Authorize( "a", "b", "c" );

            Assert.Equal(200, result.StatusCode );
        }
    }

    internal class GoodAuthResponse : IRestResponse<GithubOauthResponse>
    {

        public GoodAuthResponse()
        {
            Data = new GithubOauthResponse();
        }

        public GithubOauthResponse Data
        {
            get;
            set; 
        }

        public string Content
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ContentEncoding
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public long ContentLength
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ContentType
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<RestResponseCookie> Cookies
        {
            get { throw new NotImplementedException(); }
        }

        public Exception ErrorException
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ErrorMessage
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<Parameter> Headers
        {
            get { throw new NotImplementedException(); }
        }

        public byte[] RawBytes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IRestRequest Request
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ResponseStatus ResponseStatus
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Uri ResponseUri
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Server
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public System.Net.HttpStatusCode StatusCode
        {
            get
            {
                return System.Net.HttpStatusCode.OK;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string StatusDescription
        {
            get
            {
                return String.Empty;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    internal class BadAuthResponse : IRestResponse<GithubOauthResponse>
    {

        public GithubOauthResponse Data
        { 
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Content
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ContentEncoding
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public long ContentLength
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ContentType
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<RestResponseCookie> Cookies
        {
            get { throw new NotImplementedException(); }
        }

        public Exception ErrorException
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ErrorMessage
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<Parameter> Headers
        {
            get { throw new NotImplementedException(); }
        }

        public byte[] RawBytes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IRestRequest Request
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ResponseStatus ResponseStatus
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Uri ResponseUri
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Server
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public System.Net.HttpStatusCode StatusCode
        {
            get
            {
                return System.Net.HttpStatusCode.NotFound;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string StatusDescription
        {
            get
            {
                return String.Empty;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
