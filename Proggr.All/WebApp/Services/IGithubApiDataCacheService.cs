namespace WebApp.Services
{
    public interface IGithubApiDataCacheService
    {
        T GetApiData<T>(string userName, string keyName);
        string GetApiData(string userName, string keyName);

        void StoreApiData(string username, string keyName, string data);
        void StoreApiData(string username, string keyName, object data);
    }
}