using Proggr.Data.Models;

namespace Worker.Repositories
{
    public interface IFileRepository
    {
        FileData AddFile(FileData file);
    }
}