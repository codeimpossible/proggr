using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace Proggr.OAuth
{
    public interface IGithubAuthClient
    {
        GithubOauthResponse Authorize( string clientId, string clientSecret, string token );
    }

    public interface IGithubApiClient
    {
        GithubProfile GetProfile( string token );
    }
}