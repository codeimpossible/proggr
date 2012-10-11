using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace Proggr.OAuth
{
    public class GithubApiClient : IGithubApiClient
    {
        private RestClient _apiClient;

        public GithubProfile GetProfile( string token )
        {
            _apiClient = new RestClient( "https://api.github.com" );

            var apiRequest = new RestRequest( "/user?access_token=" + token, Method.GET );

            var apiResponse = _apiClient.Execute<GithubProfile>( apiRequest );

            return apiResponse.Data;
        }
    }
}