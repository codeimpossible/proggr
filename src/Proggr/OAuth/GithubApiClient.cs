using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace Proggr.OAuth
{
    public class GithubApiClient : IGithubApiClient
    {
        private IRestClient _apiClient;

        public GithubApiClient() : this(null) { }
        public GithubApiClient( IRestClient apiClient )
        {
            _apiClient = apiClient ?? new RestClient();
            _apiClient.BaseUrl = "http://api.github.com";
        }

        public GithubProfile GetProfile( string token )
        {
            var apiRequest = new RestRequest( "/user?access_token=" + token, Method.GET );

            var apiResponse = _apiClient.Execute<GithubProfile>( apiRequest );

            return apiResponse == null ? new EmptyGithubProfile() : apiResponse.Data;
        }
    }
}