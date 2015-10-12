using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Worker.Controllers
{
    public static class RepositoryController
    {
        public static async Task<Commit> GetCommit(Repository repo, string sha)
        {
            return await new Task<Commit>( () => repo.Commits.QueryBy(new CommitFilter() {Since = sha, SortBy = CommitSortStrategies.Reverse}).Take(1).FirstOrDefault() );
        }
    }
}
