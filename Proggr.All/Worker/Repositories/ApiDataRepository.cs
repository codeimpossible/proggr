using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace Worker.Repositories
{
    public class ApiDataRepository : SimpleDataRepository, IApiDataRepository
    {
        public string GetUserToken(string username)
        {
            var result = Database.GithubJsonData.FindAllByGithubUserName(GithubUserName: username).FirstOrDefault();
            return result == null ? null : result.ApiToken;
        }
    }
}
