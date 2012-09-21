using System;
using System.Diagnostics;
using System.Web;
using System.Web.Security;

namespace Proggr.OAuth
{
    /// <summary>
    /// Helper methods for setting and retrieving a custom forms authentication ticket for delegation protocols.
    /// </summary>
    public static class OpenAuthAuthenticationTicketHelper
    {
        /// <summary>
        /// The open auth cookie token.
        /// </summary>
        private const string OpenAuthCookieToken = "OAuth";


        /// <summary>
        /// Checks whether the specified HTTP request comes from an authenticated user.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>True if the reuest is authenticated; false otherwise.</returns>
        public static bool IsValidAuthenticationTicket( HttpContextBase context )
        {
            HttpCookie cookie = context.Request.Cookies[ FormsAuthentication.FormsCookieName ];
            if( cookie == null )
            {
                return false;
            }

            string encryptedCookieData = cookie.Value;
            if( string.IsNullOrEmpty( encryptedCookieData ) )
            {
                return false;
            }

            try
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt( encryptedCookieData );
                return authTicket != null && !authTicket.Expired && authTicket.Name == OpenAuthCookieToken;
            }
            catch( ArgumentException )
            {
                return false;
            }
        }

        /// <summary>
        /// Adds an authentication cookie to the user agent in the next HTTP response.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="createPersistentCookie">
        /// A value indicating whether the cookie should persist across sessions.
        /// </param>
        public static void SetAuthenticationTicket( HttpContextBase context, string userName, bool createPersistentCookie )
        {
            if( !context.Request.IsSecureConnection && FormsAuthentication.RequireSSL )
            {
                throw new HttpException( "Connection is not secure" );
            }

            HttpCookie cookie = GetAuthCookie( userName, createPersistentCookie );
            context.Response.Cookies.Add( cookie );
        }

        public static string GetCurrentUsersLogin()
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies[ OpenAuthCookieToken ];
            if( cookie != null )
            {

                var val = cookie.Value;
                var ticket = FormsAuthentication.Decrypt( val );

                if( ticket != null )
                {
                    return ticket.UserData;                    
                }
            }

            return null;
        }


        public static void RemoveUserCookie()
        {
            System.Web.HttpContext.Current.Request.Cookies.Remove( OpenAuthCookieToken );

            var ticketExpiration = DateTime.Now.AddDays( -7 );
            var ticket = new FormsAuthenticationTicket( 1, OpenAuthCookieToken, DateTime.Now, ticketExpiration, false, String.Empty );
            var cookie = new System.Web.HttpCookie( OpenAuthCookieToken )
            {
                Expires = ticketExpiration,
                Value = FormsAuthentication.Encrypt( ticket ),
                HttpOnly = true
            };

            System.Web.HttpContext.Current.Response.Cookies.Add( cookie );
        }

        /// <summary>
        /// Creates an HTTP authentication cookie.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="createPersistentCookie">
        /// A value indicating whether the cookie should last across sessions.
        /// </param>
        /// <returns>An authentication cookie.</returns>
        private static HttpCookie GetAuthCookie( string userName, bool createPersistentCookie )
        {
            if( String.IsNullOrWhiteSpace( userName ) )
            {
                throw new ArgumentException( "userName" );
            }

            var ticket = new FormsAuthenticationTicket(
                /* version */
                2,
                OpenAuthCookieToken,
                DateTime.Now,
                DateTime.Now.Add( FormsAuthentication.Timeout ),
                createPersistentCookie,
                userName,
                FormsAuthentication.FormsCookiePath );

            string encryptedTicket = FormsAuthentication.Encrypt( ticket );
            if( encryptedTicket == null || encryptedTicket.Length < 1 )
            {
                throw new HttpException( "Failed to encrypt ticket" );
            }

            var cookie = new HttpCookie( FormsAuthentication.FormsCookieName, encryptedTicket )
            {
                HttpOnly = true,
                Path = FormsAuthentication.FormsCookiePath
            };

            // only set Secure if FormsAuthentication requires SSL. 
            // otherwise, leave it to default value
            if( FormsAuthentication.RequireSSL )
            {
                cookie.Secure = true;
            }

            if( FormsAuthentication.CookieDomain != null )
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            if( ticket.IsPersistent )
            {
                cookie.Expires = ticket.Expiration;
            }

            return cookie;
        }
    }
}