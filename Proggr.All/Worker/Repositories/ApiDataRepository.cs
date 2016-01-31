using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Newtonsoft.Json;
using Proggr.Data;
using Proggr.Data.Models;
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

        public List<string> GetRepositories(string username)
        {
            var result =
                Database.GithubApiData.FindAllByUserNameAndKeyName(UserName: username, KeyName: ApiStorageConstants.APIDATA_KEY_REPOSITORIES).FirstOrDefault();

            return JsonConvert.DeserializeObject<List<GithubApiRepository>>(result);
        }
    }
}
