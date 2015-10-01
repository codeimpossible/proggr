using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp;
using WebApp.Modules;

[assembly: PreApplicationStartMethod(typeof(PreStartCode), "Start")]
namespace WebApp
{
    public class PreStartCode
    {
        public static void Start()
        {
            HttpApplication.RegisterModule(typeof(RequestIdentifierModule));
        }
    }
}