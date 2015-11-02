namespace Proggr.Data
{
    public class SimpleDataRepository
    {
        public SimpleDataRepository()
        {
            Database = Simple.Data.Database.OpenNamedConnection("DefaultConnection");
        }

        protected dynamic Database { get; private set; }
    }
}
