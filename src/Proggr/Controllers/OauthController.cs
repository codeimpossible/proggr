using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Proggr.Configuration;
using Proggr.Models;
using Proggr.OAuth;
using RestSharp;
using Simple.Data;

namespace Proggr.Controllers {
    public class OauthController : ControllerBase {
        private IGithubAuthClient _authClient;
        private IGithubApiClient _apiClient;

        public OauthController() : this( null, null, null ) { }
        public OauthController(
            IGithubAuthClient authClient = null,
            IGithubApiClient apiClient = null,
            ConfigurationSettings settings = null,
            TicketHelper ticketHelper = null )
            : base( settings, ticketHelper ) {
            _authClient = authClient ?? new GithubAuthorizationClient();
            _apiClient = apiClient ?? new GithubApiClient();
        }

        /// <summary>
        /// Github OAuth Callback
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult Callback( string code ) {
            var clientId = _configuration.OAuthClientKey;
            var clientSecret = _configuration.OAuthClientSecret;

            var githubResponse = _authClient.Authorize( clientId, clientSecret, code );

            if( !githubResponse.IsError ) {
                // we're in...

                var db = OpenDatabaseConnection();

                var profile = _apiClient.GetProfile( githubResponse.AccessToken );

                // does the user already exist in the db?
                var user = db.Users.Find( db.Users.login == profile.Login );
                if( user == null ) {
                    // insert this into the DB
                    db.Users.Insert( new {
                        login = profile.Login,
                        avatar_url = profile.AvatarUrl,
                        name = profile.Name
                    } );
                }

                // set the user into the Thread Security
                var securityUser = new WebsiteUser() {
                    Login = profile.Login,
                    AvatarUrl = profile.AvatarUrl,
                    Name = profile.Name
                };

                _ticketHelper.SetUserCookie( securityUser, true );
                _ticketHelper.SetAuthCookie( securityUser, true );

                return RedirectToAction( "Details", new { controller = "Profiles", id = profile.Login } );
            }

            return RedirectToAction( "AuthError", new { controller = "Errors" } );
        }
    }
}
