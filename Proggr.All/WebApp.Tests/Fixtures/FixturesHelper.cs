using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Tests.Fixtures
{
    public static class FixturesHelper
    {
        public static List<MODEL> StoreFakes<MODEL>(Action<MODEL> modelRepository , int count, Func<MODEL> modelFactory)
        {
            var list = new List<MODEL>();
            for (var i = 0; i < count; i++)
            {
                var model = modelFactory();

                list.Add(model);

                modelRepository(model);
            }
            return list;
        }

        public static MODEL StoreFake<MODEL>(Action<MODEL> modelRepository, Func<MODEL> modelFactory)
        {
            var model = modelFactory();

            modelRepository(model);
            return model;
        }
    }
}
