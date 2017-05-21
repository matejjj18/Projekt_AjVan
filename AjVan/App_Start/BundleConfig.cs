using System.Web;
using System.Web.Optimization;

namespace AjVan
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

            #region boostrap-datetimepicker
            bundles.Add(new ScriptBundle("~/bundles/js/boostrap-datetimepicker").Include(
            "~/Content/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js"));

            bundles.Add(new StyleBundle("~/bundles/css/boostrap-datetimepicker").Include(
  "~/Content/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css"));
            #endregion /bootstrap-datetimepicker


            #region moment
            bundles.Add(new ScriptBundle("~/bundles/js/moment").Include(
            "~/Content/plugins/moment/moment.min.js"));
            #endregion /moment
        }
    }
}
