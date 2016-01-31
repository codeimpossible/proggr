using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var app = new ScriptBundle("~/bundles/app", "http://localhost:9090/webpack/app-bundle.js")
                            .Include("~/UX/dist/app-bundle.js");
#if DEBUG
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;
            app.CdnFallbackExpression = "true";
#else
            app.CdnFallbackExpression = "false";
#endif
            bundles.Add(app);
            
            // use bootstrap for now
            bundles.Add(new StyleBundle("~/bundles/basestyles")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/site.base.css"));
        }
    }
}
