namespace Proggr.Data
{
    public abstract class SimpleDataRepository
    {
        protected SimpleDataRepository()
        {
            Database = Simple.Data.Database.OpenNamedConnection("DefaultConnection");
        }

        protected dynamic Database { get; private set; }
    }
}
