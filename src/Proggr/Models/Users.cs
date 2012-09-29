using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proggr.Models
{
    public class WebsiteUser
    {
        public static readonly WebsiteUser Guest = new WebsiteUser { Login = "Guest", AvatarUrl = "", Name = "Guest" };

        public string Login { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
    }

    public class SecurityUser : WebsiteUser, System.Security.Principal.IPrincipal
    {
        public System.Security.Principal.IIdentity Identity
        {
            get
            {
                return new UserIdentity( !( this.Login == "Guest" ), this.Login);
            }
        }

        public bool IsInRole( string role )
        {
            return true;
        }
    }

    public class UserIdentity : System.Security.Principal.IIdentity
    {
        public UserIdentity( bool isAuthenticated, string name )
        {
            IsAuthenticated = isAuthenticated;
            Name = name;
        }

        public string AuthenticationType { get { return "Forms"; } }

        public bool IsAuthenticated { get; private set; }

        public string Name { get; private set; }
    }

}