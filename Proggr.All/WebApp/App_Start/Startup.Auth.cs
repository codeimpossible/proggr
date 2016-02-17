using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Owin.Security.Providers.GitHub;
using Owin;
using Proggr.Data;
using Simple.Data;
using WebApp.Data;
using WebApp.Models;
using WebApp.Providers;
using WebApp.Services;

namespace WebApp
{
    public partial class Startup
    {
        private readonly IGithubApiDataCacheService _apiDataCacheService;

        public Startup() : this(null) { }

        public Startup(IGithubApiDataCacheService apiDataCacheService)
        {
            _apiDataCacheService = apiDataCacheService ?? new GithubApiDataCacheService();
        }

        static Startup()
        {
            PublicClientId = "web";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            //// Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseOAuthBearerTokens(OAuthOptions);

            var options = new GitHubAuthenticationOptions
            {
                // TODO: move these to an external config file
                ClientId = WebApp.Configuration.Current().GetClientId(),
                ClientSecret = WebApp.Configuration.Current().GetClientSecret(),
                Provider = new GitHubAuthenticationProvider
                {
#pragma warning disable 1998
                    OnAuthenticated = async context =>
                    {
                        try
                        {
                            _apiDataCacheService.StoreApiData(context.UserName, ApiStorageConstants.APIDATA_KEY_APITOKEN, context.AccessToken);
                            // TODO: prefetch some data?
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
#pragma warning restore 1998
                },
            };
            options.Scope.Add("repo");
            options.Scope.Add("user");
            options.Scope.Add("user:email");
            app.UseGitHubAuthentication(options);
        }
    }
}
