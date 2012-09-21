using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Proggr.Models;
using WebMatrix.WebData;

namespace Proggr.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private Users _usersTable = new Users();

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction( "Index", "Home" );
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin( string provider, string returnUrl )
        {
            return new ExternalLoginResult( provider, Url.Action( "ExternalLoginCallback", new { ReturnUrl = "http://localhost/profiles/codeimpossible" } ) );
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback( string returnUrl )
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication( Url.Action( "ExternalLoginCallback", new { ReturnUrl = returnUrl } ) );
            if( !result.IsSuccessful )
            {
                return RedirectToAction( "ExternalLoginFailure" );
            }

            if( OAuthWebSecurity.Login( result.Provider, result.ProviderUserId, createPersistentCookie: false ) )
            {
                return RedirectToLocal( returnUrl );
            }

            if( User.Identity.IsAuthenticated )
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount( result.Provider, result.ProviderUserId, User.Identity.Name );
            }
            return RedirectToLocal( returnUrl );
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation( RegisterExternalLoginModel model, string returnUrl )
        {
            string provider = null;
            string providerUserId = null;

            if( User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId( model.ExternalLoginData, out provider, out providerUserId ) )
            {
                return RedirectToAction( "Manage" );
            }

            if( ModelState.IsValid )
            {
                // Insert a new user into the database
                var users = _usersTable.All( where: "login = @0", args: "test" );
                if( users.Count() == 0 )
                {
                    // insert this into the DB
                    //_usersTable.Insert( new
                    //{
                    //    login = profile.Login,
                    //    avatar_url = profile.AvatarUrl,
                    //    name = profile.Name
                    //} );

                    OAuthWebSecurity.CreateOrUpdateAccount( provider, providerUserId, model.UserName );
                    OAuthWebSecurity.Login( provider, providerUserId, createPersistentCookie: false );

                    return RedirectToLocal( returnUrl );
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData( provider ).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View( model );
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        private ActionResult RedirectToLocal( string returnUrl )
        {
            if( Url.IsLocalUrl( returnUrl ) )
            {
                return Redirect( returnUrl );
            }
            else
            {
                return RedirectToAction( "Index", "Home" );
            }
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult( string provider, string returnUrl )
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult( ControllerContext context )
            {
                OAuthWebSecurity.RequestAuthentication( Provider, ReturnUrl );
            }
        }
    }

    public class RegisterExternalLoginModel
    {
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
