using LibGit2Sharp;
using Worker.Models;

namespace Worker.Repositories
{
    public interface IFileRepository
    {
        FileData AddFile(FileData file);
    }
}