using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Worker.Controllers
{
    public static class CommitController
    {
        public static async Task<Patch> GetChangeSet(Repository repo, Commit commit)
        {
            return await new Task<Patch>(() =>
            {
                var commitTree = commit.Tree; // Main Tree
                var parentCommitTree = commit.Parents.Single().Tree; // Secondary Tree

                return repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference
            });
        }
    }
}
