using System;
using Encryptamajig;
using Newtonsoft.Json;
using WebApp.Data;

namespace WebApp.Services
{
    public class GithubApiDataCacheService : IGithubApiDataCacheService
    {
        private readonly dynamic _database;
        public GithubApiDataCacheService()
        {
            _database = Storage.CreateConnection();
        }

        public T GetApiData<T>(string username, string keyName)
        {
            var row = _database.GitHubApiData.FindAllByUserNameAndKeyName(UserName: username, KeyName: keyName).FirstOrDefault();
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

        public string GetApiData(string username, string keyName)
        {
            var row = _database.GitHubApiData.FindAllByUserNameAndKeyName(UserName: username, KeyName: keyName).FirstOrDefault();
            if (row != null)
            {
                // TODO: use secure string? or am I being too paranoid?
                var encryptedData = row.DataRaw;
                var raw = AesEncryptamajig.Decrypt(encryptedData, username + Configuration.Current().GetClientSecret());
                return raw;
            }
            return null;
        }

        public void StoreApiData(string username, string keyName, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            StoreApiData(username, keyName, json);
        }

        public void StoreApiData(string username, string keyName, string data)
        {
            var encryptedData = AesEncryptamajig.Encrypt(data, username + Configuration.Current().GetClientSecret());
            var row = _database.GitHubApiData.FindAllByUserNameAndKeyName(UserName: username, KeyName: keyName).FirstOrDefault();
            if (row != null)
            {
                _database.GitHubApiData.UpdateByUserNameAndKeyName(UserName: username, KeyName: keyName,
                    DataRaw: encryptedData, DateUpdated: DateTime.UtcNow);
            }
            else
            {
                _database.GitHubApiData.Insert(UserName: username, KeyName: keyName,
                    DataRaw: encryptedData, DateUpdated: DateTime.UtcNow, DateCreated: DateTime.UtcNow);
            }
        }
    }
}