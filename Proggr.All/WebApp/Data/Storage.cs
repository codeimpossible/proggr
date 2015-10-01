using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Octokit;
using Simple.Data;
using Newtonsoft.Json;

namespace WebApp.Data
{
    // TODO: cache this for the current request
    public static class Storage
    {
        public static dynamic Current()
        {
            // TODO: i think HttpContext.Current.Cache is better...
            if (HttpContext.Current.Items["RequestId"] != null)
            {
                var id = HttpContext.Current.Items["RequestId"];
                var cacheId = "storage-db" + id;
                if (HttpRuntime.Cache[cacheId] != null)
                {
                    return HttpRuntime.Cache[cacheId];
                }

                var db = CreateConnection();
                HttpRuntime.Cache.Add(cacheId, db, null, DateTime.Now.AddMinutes(30.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                return db;
            }
            return CreateConnection();
        }

        private static dynamic CreateConnection()
        {
            const string connectionName = "DefaultConnection";
            return Database.OpenNamedConnection(connectionName);
        }

        public static void StoreRepositoryJson(string username, IReadOnlyList<Repository> repositories)
        {
            var json = JsonConvert.SerializeObject(repositories);
            var storage = Storage.Current();

            storage.GithubJsonData.UpdateByGithubUserName(GithubUserName: username, ReposJsonData: json, DateUpdated: DateTime.UtcNow);
        }
    }
}