using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Proggr.Models;
using Proggr.OAuth;
using RestSharp;
using Simple.Data;

namespace Proggr.Controllers
{
    public class OauthController : DataControllerBase
    {
        /// <summary>
        /// Github OAuth Callback
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult Callback( string code )
        {
            var clientId = ConfigurationManager.AppSettings["github.oauth.clientkey"];
            var clientSecret = ConfigurationManager.AppSettings[ "github.oauth.secret" ];
            
            var client = new RestClient( "https://github.com/" );
            var request = new RestRequest( "login/oauth/access_token", Method.POST );

            request.AddParameter( "client_id", clientId )
                   .AddParameter( "client_secret", clientSecret )
                   .AddParameter( "code", code );

            var response = client.Execute<GithubOauthResponse>( request );

            if( response.StatusCode == System.Net.HttpStatusCode.OK )
            {
                // we're in...

                var db = OpenDatabaseConnection();

                var apiClient = new RestClient( "https://api.github.com" );
                var apiRequest = new RestRequest( "/user?access_token=" + response.Data.AccessToken, Method.GET );

                var apiResponse = apiClient.Execute<GithubProfile>( apiRequest );

                var profile = apiResponse.Data;

                // does the user already exist in the db?
                var users = db.Users.Find( db.Users.login == profile.Login );
                if( users.Count() == 0 )
                {
                    // insert this into the DB
                    db.Users.Insert( new
                    {
                        login = profile.Login,
                        avatar_url = profile.AvatarUrl,
                        name = profile.Name
                    } );
                }

                // set the user into the Thread Security
                var securityUser = new WebsiteUser()
                {
                    Login = profile.Login,
                    AvatarUrl = profile.AvatarUrl,
                    Name = profile.Name
                };

                OAuthTicketHelper.SetUserCookie( securityUser, true );
                OAuthTicketHelper.SetAuthCookie( securityUser, true );

                return RedirectToAction( "Details", new { controller = "Profiles", id = profile.Login } );
            }

            return RedirectToAction( "AuthError", new { controller = "Errors" } );
        }
    }


    public class GithubOauthResponse
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
    }

    public class GithubProfile
    {
        public string Login { get; set; }
        public string Id { get; set; }
        public string AvatarUrl { get; set; }
        public string GravatarId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public bool Hireable { get; set; }
        public string Bio { get; set; }
        public int PublicRepos { get; set; }
        public int PublicGists { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string HtmlUrl { get; set; }
        public string CreatedAt { get; set; }
        public string Type { get; set; }
    }
}
