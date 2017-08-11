using System.Web;
using System.Web.Optimization;

namespace fehteam.app
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        
            bundles.Add(new ScriptBundle("~/jsre").Include(
                      "~/Scripts/jsre/lib/*.js",
                      "~/Scripts/jsre/init.js",
                      "~/Scripts/jsre/core/*.js"));


            bundles.Add(new ScriptBundle("~/script/f7").Include(
                        "~/Scripts/framework7.js"));

            bundles.Add(new StyleBundle("~/css/f7").Include(
                      "~/Content/framework7.material.css",
                      "~/Content/framework7.material.colors.css",
                      "~/Content/font-roboto.css",
                      "~/Content/font-material-icons.css"));


            bundles.Add(new ScriptBundle("~/script/jqueryui").Include("~/Scripts/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/css/jqueryui").Include("~/Content/jquery-ui/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/script/tabulator").Include("~/Scripts/tabulator.js"));

            bundles.Add(new StyleBundle("~/css/tabulator").Include("~/Content/tabulator.css"));
        }
    }
}
