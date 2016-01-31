using Proggr.Data;
using Proggr.Data.Models;

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
