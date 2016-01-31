using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Worker.Controllers
{
    public class RepositoryController : IRepositoryController
    {
        public async Task<Commit> GetCommit(Repository repo, string sha)
        {
            return await new Task<Commit>( () => repo.Commits.QueryBy(new CommitFilter() {Since = sha, SortBy = CommitSortStrategies.Reverse}).Take(1).FirstOrDefault() );
        }

        public async Task<string> Clone(string url, string workingDirectory, CloneOptions options = null)
        {
            return await new Task<string>(() => Repository.Clone(url, workingDirectory, options));
        }
    }
}
