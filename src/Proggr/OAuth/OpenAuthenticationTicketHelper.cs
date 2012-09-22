using System;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using Proggr.Models;
using WebMatrix.WebData;

namespace Proggr.OAuth
{
    /// <summary>
    /// Helper methods for setting and retrieving a custom forms authentication ticket for delegation protocols.
    /// </summary>
    public static class OAuthTicketHelper
    {
        /// <summary>
        /// The open auth cookie token.
        /// </summary>
        private const string OpenAuthCookieToken = "OAuth";


        public static void SetCookie( string data, string name, bool createPersistentCookie )
        {
            var ticketExpiration = DateTime.Now.AddMonths( 7 );
            var ticket = new FormsAuthenticationTicket( 1, name, DateTime.Now, ticketExpiration, createPersistentCookie, data );
            var cookie = new HttpCookie( name )
            {
                Expires = ticketExpiration,
                Value = FormsAuthentication.Encrypt( ticket ),
                HttpOnly = true
            };

            System.Web.HttpContext.Current.Response.Cookies.Add( cookie );
        }


        public static void SetUserCookie( WebsiteUser user, bool createPersistentCookie )
        {
            string userFormat = "{0}|{1}|{2}";
            if( GetUserFromCookie() != null )
            {
                System.Web.HttpContext.Current.Request.Cookies.Remove( OpenAuthCookieToken );
            }

            SetCookie( String.Format( userFormat, user.Login, user.Name, user.AvatarUrl), OpenAuthCookieToken, true );
        }


        public static WebsiteUser GetUserFromCookie()
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies[ OpenAuthCookieToken ];
            WebsiteUser returningUser = WebsiteUser.Guest;

            if( cookie != null )
            {

                var val = cookie.Value;
                var ticket = FormsAuthentication.Decrypt( val );

                if( ticket != null )
                {

                    var ticketData = ticket.UserData.Split( '|' );

                    if( ticketData.Length > 1 )
                    {

                        returningUser = new WebsiteUser()
                        {
                            Login = ticketData[ 0 ],
                            Name = ticketData[ 1 ],
                            AvatarUrl = ticketData[ 2 ]
                        };
                    }
                }
            }

            return returningUser;
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

            HttpContext.Current.User = null;

            System.Threading.Thread.CurrentPrincipal = null;
        }


        public static void SetAuthCookie( WebsiteUser user, bool createPersistentCookie )
        {
            FormsAuthentication.SetAuthCookie( user.Login, createPersistentCookie );

            var securityUser = new SecurityUser
            {
                Login = user.Login,
                AvatarUrl = user.AvatarUrl,
                Name = user.Name
            };

            HttpContext.Current.User = securityUser;

            System.Threading.Thread.CurrentPrincipal = securityUser;
        }
    }
}