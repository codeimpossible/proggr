using LibGit2Sharp;
using Worker.Models;

namespace Worker.Repositories
{
    public class FileRepository : SimpleDataRepository, IFileRepository
    {
        public FileData AddFile(FileData file)
        {
            return Database.Files.Insert(file);
        }
    }
}
