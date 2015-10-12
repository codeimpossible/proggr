using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Encryptamajig;
using Octokit;
using Simple.Data;
using Newtonsoft.Json;

namespace WebApp.Data
{
    public static class Storage
    {
        public const string APIDATA_KEY_APITOKEN = "ApiToken";
        public const string APIDATA_KEY_USER = "User";
        public const string APIDATA_KEY_REPOSITORIES = "Repositories";

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

        public static void StoreApiData(string username, string keyName, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            StoreApiData(username, keyName, json);
        }

        public static void StoreApiData(string username, string keyName, string data)
        {
            var storage = Storage.Current();
            var encryptedData = AesEncryptamajig.Encrypt(data, username + Configuration.Current().GetClientSecret());
            var row = storage.GitHubApiData.FindAllByUserNameAndKeyName(UserName: username, KeyName: keyName).FirstOrDefault();
            if (row != null)
            {
                storage.GitHubApiData.UpdateByUserNameAndKeyName(UserName: username, KeyName: keyName,
                    DataRaw: encryptedData, DateUpdated: DateTime.UtcNow);
            }
            else
            {
                storage.GitHubApiData.Insert(UserName: username, KeyName: keyName,
                    DataRaw: encryptedData, DateUpdated: DateTime.UtcNow, DateCreated: DateTime.UtcNow);
            }
        }

        public static T GetApiData<T>(string username, string keyName)
        {
            var storage = Storage.Current();
            var row = storage.GitHubApiData.FindAllByUserNameAndKeyName(UserName: username, KeyName: keyName).FirstOrDefault();
            if (row != null)
            {
                // TODO: use secure string? or am I being too paranoid?
                var encryptedData = row.DataRaw;
                var raw = AesEncryptamajig.Decrypt(encryptedData, username + Configuration.Current().GetClientSecret());
                var result = JsonConvert.DeserializeObject<T>(raw);
                return result;
            }
            return default(T);
        }

        public static string GetApiData(string username, string keyName)
        {
            var storage = Storage.Current();
            var row = storage.GitHubApiData.FindAllByUserNameAndKeyName(UserName: username, KeyName: keyName).FirstOrDefault();
            if (row != null)
            {
                // TODO: use secure string? or am I being too paranoid?
                var encryptedData = row.DataRaw;
                var raw = AesEncryptamajig.Decrypt(encryptedData, username + Configuration.Current().GetClientSecret());
                return raw;
            }
            return null;
        }

        public static void StoreRepositoryJson(string username, IReadOnlyList<Repository> repositories)
        {
            var json = JsonConvert.SerializeObject(repositories);
            var storage = Storage.Current();

            storage.GithubJsonData.UpdateByGithubUserName(GithubUserName: username, ReposJsonData: json, DateUpdated: DateTime.UtcNow);
        }
    }
}