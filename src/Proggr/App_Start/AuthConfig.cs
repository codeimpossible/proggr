using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using Proggr.OAuth;

namespace Proggr
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            var clientKey = ConfigurationManager.AppSettings["github.oauth.clientkey"];
            var clientSecret = ConfigurationManager.AppSettings["github.oauth.secret"];

            OAuthWebSecurity.RegisterClient( new GithubClient( clientKey, clientSecret ), "Github Open Auth", null );
        }
    }
}
