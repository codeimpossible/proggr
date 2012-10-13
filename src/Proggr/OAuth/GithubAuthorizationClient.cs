using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using RestSharp;

namespace Proggr.OAuth
{
    public class GithubAuthorizationClient : IGithubAuthClient
    {
        private IRestClient _restClient;

        public GithubAuthorizationClient( IRestClient restClient = null )
        {
            _restClient = restClient ?? new RestClient();
            _restClient.BaseUrl = "https://github.com/";
        }

        public GithubOauthResponse Authorize( string clientId, string clientSecret, string token )
        {
            var request = new RestRequest( "login/oauth/access_token", Method.POST );

            request.AddParameter( "client_id", clientId )
                   .AddParameter( "client_secret", clientSecret )
                   .AddParameter( "code", token );

            var response = _restClient.Execute<GithubOauthResponse>( request );

            if( response.StatusCode == System.Net.HttpStatusCode.OK )
            {
                response.Data.StatusCode = (int)response.StatusCode;
                return response.Data;
            }

            return GithubOauthResponse.Error( (int)response.StatusCode, response.StatusDescription );
        }
    }
}