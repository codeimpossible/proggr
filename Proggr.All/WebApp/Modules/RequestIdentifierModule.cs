using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Modules
{
    public class RequestIdentifierModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        public void OnBeginRequest(Object sender, EventArgs e)
        {
            var httpApp = (HttpApplication)sender;
            var id = Guid.NewGuid().ToString().Substring(0, 8);
            HttpRuntime.Cache["RequestId"] = httpApp.Context.Items["RequestId"] = id;
        }

        public void Dispose()
        {
            
        }
    }
}