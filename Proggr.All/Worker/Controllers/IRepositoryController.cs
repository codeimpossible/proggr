using System.Threading.Tasks;
using LibGit2Sharp;

namespace Worker.Controllers
{
    public interface IRepositoryController
    {
        Task<Commit> GetCommit(Repository repo, string sha);

        Task<string> Clone(string url, string workingDirectory, CloneOptions options = null);
    }
}