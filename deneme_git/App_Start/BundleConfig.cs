using System.Web;
using System.Web.Optimization;

namespace CloudNoteV1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/Content/site/css").Include(
                        "~/Content/bootstrap/bootstrap.css",
                        "~/Content/_css/font-awesome.css",
                        "~/Content/_css/site.css"
                    ));

            bundles.Add(new StyleBundle("~/Content/summernote").Include(
               "~/Content/summernote/summernote.css"
               ));
            bundles.Add(new ScriptBundle("~/bundles/summernote").Include(
               "~/Content/summernote/summernote.js"
               ));

        }
    }
}
