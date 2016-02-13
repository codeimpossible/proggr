using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApp.Tests
{
    public static class JsonResultExtensions
    {
        private static readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();
        public static T DeserializeData<T>(this JsonResult result)
        {
            var json = _serializer.Serialize(result.Data);
            return _serializer.Deserialize<T>(json);
        }
    }
}
