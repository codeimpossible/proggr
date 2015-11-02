using System.Collections.Generic;

namespace Worker.Repositories
{
    public interface IApiDataRepository
    {
        string GetUserToken(string username);

        List<string> GetRepositories(string username);
    }
}