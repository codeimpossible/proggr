using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace WebApp.Areas.Api.Models
{
    public class Repo
    {
        public string Url { get; set; }

        public string HtmlUrl { get; set; }

        public string CloneUrl { get; set; }

        public string GitUrl { get; set; }

        public string SshUrl { get; set; }

        public string SvnUrl { get; set; }

        public string MirrorUrl { get; set; }

        public int Id { get; set; }

        public User Owner { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string Homepage { get; set; }

        public string Language { get; set; }

        public bool Private { get; set; }

        public bool Fork { get; set; }

        public int ForksCount { get; set; }

        public int StargazersCount { get; set; }

        public int SubscribersCount { get; set; }

        public string DefaultBranch { get; set; }

        public int OpenIssuesCount { get; set; }

        public DateTimeOffset? PushedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public bool HasIssues { get; set; }

        public bool HasWiki { get; set; }

        public bool HasDownloads { get; set; }
    }
}