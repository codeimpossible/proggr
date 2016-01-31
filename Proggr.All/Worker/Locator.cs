using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    public interface ILocator
    {
        T Locate<T>();
    }

    public interface IRegistry
    {
        void Register<T>(object concrete);
    }


    public class Locator : ILocator, IRegistry
    {
        private readonly IDictionary<Type, object> _concreteToInterfaceMap = new Dictionary<Type, object>();

        public void Register<T>(object concrete)
        {
            _concreteToInterfaceMap.Add(typeof(T), concrete);
        }

        public T Locate<T>()
        {
            return _concreteToInterfaceMap.ContainsKey(typeof (T)) ? (T) _concreteToInterfaceMap[typeof (T)] : default(T);
        }
    }
}
