namespace Worker.Repositories
{
    public interface IApiDataRepository
    {
        string GetUserToken(string username);
    }
}