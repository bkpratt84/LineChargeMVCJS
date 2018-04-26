using System.Web;
using System.Web.Optimization;

namespace LineChargeJS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/vuelidate").Include(
                       "~/Scripts/vuelidate/validators.min.js",
                       "~/Scripts/vuelidate/vuelidate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                       "~/Scripts/moment.min.js",
                       "~/Scripts/accounting.min.js",
                       "~/Scripts/axios.js",
                       "~/Scripts/vue.js",
                       "~/Scripts/Home/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/umd/popper.min.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/Site.css"));
        }
    }
}
