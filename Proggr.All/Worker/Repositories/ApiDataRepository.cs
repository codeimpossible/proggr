using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace Worker.Repositories
{
    public class ApiDataRepository : IApiDataRepository
    {
        private readonly dynamic _database;

        public ApiDataRepository()
        {
            _database = Database.OpenNamedConnection("DefaultConnection");
        }

        public string GetUserToken(string username)
        {
            var result = _database.GithubJsonData.FindAllByGithubUserName(GithubUserName: username).FirstOrDefault();
            return result == null ? null : result.ApiToken;
        }
    }
}
