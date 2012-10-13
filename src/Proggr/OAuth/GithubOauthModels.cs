using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proggr.OAuth
{
    public class GithubOauthResponse
    {
        public static GithubOauthResponse Error( int statuscode, string message )
        {
            return new GithubOauthResponse { StatusCode = statuscode, ResponseMessage = message };
        }

        public string TokenType { get; set; }
        public string AccessToken { get; set; }

        public int StatusCode { get; set; }
        public string ResponseMessage { get; set; }

        public bool IsError { get { return StatusCode != 200; } }
    }

    public class EmptyGithubProfile : GithubProfile
    {
        public override bool IsEmpty
        {
            get
            {
                return true;
            }
        }
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

        public virtual bool IsEmpty { get; private set; }
    }
}