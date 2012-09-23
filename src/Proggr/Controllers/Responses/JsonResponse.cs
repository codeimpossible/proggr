using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proggr.Controllers.Responses
{
    public class JsonResponse : JsonResult
    {
        public dynamic Meta { get; set; }

        public JsonResponse()
        {
            Meta = new ExpandoObject();
        }

        public JsonResponse( int statusCode, dynamic model ) : this()
        {
            Meta.Status = statusCode;

            Data = new { Meta = Meta, Data = model };
            ContentEncoding = null;
            ContentType = null;
            JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.DenyGet;
        }
    }
}