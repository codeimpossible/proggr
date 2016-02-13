using Simple.Data;

namespace WebApp.Data
{
    public static class Storage
    {
        public static dynamic CreateConnection()
        {
            const string connectionName = "DefaultConnection";
            return Database.OpenNamedConnection(connectionName);
        }
    }
}