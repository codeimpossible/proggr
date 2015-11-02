using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggr.Data.Models
{
    public class GithubApiRepository
    {
        public string CloneUrl { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public string DefaultBranch { get; protected set; }
        public string Description { get; protected set; }
        public bool Fork { get; protected set; }
        public int ForksCount { get; protected set; }
        public string FullName { get; protected set; }
        public string GitUrl { get; protected set; }
        public bool HasDownloads { get; protected set; }
        public bool HasIssues { get; protected set; }
        public bool HasWiki { get; protected set; }
        public string Homepage { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public int Id { get; protected set; }
        public string Language { get; protected set; }
        public string MirrorUrl { get; protected set; }
        public string Name { get; protected set; }
        public int OpenIssuesCount { get; protected set; }
        public bool Private { get; protected set; }
        public DateTimeOffset? PushedAt { get; protected set; }
        public string SshUrl { get; protected set; }
        public int StargazersCount { get; protected set; }
        public int SubscribersCount { get; protected set; }
        public string SvnUrl { get; protected set; }
        public DateTimeOffset UpdatedAt { get; protected set; }
        public string Url { get; protected set; }
    }
}
