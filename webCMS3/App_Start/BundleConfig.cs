using System.Web;
using System.Web.Optimization;

namespace webCMS3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-3.4.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/adbootstrap").Include(
                "~/Scripts/bootstrap.bundle.min.js",
                 "~/Scripts/bootbox.min.js",
                 "~/Scripts/custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jspost").Include(
                "~/Scripts/jsContent.min.js",
                "~/Scripts/bootstrap_tagsinput.min.js"));

            bundles.Add(new StyleBundle("~/Content/cssupload").Include(
               "~/Content/jquery.fileupload.min.css",
                "~/Content/jquery.Jcrop.min.css"));

            bundles.Add(new StyleBundle("~/Content/adcss").Include(
                 "~/Content/bootstrap.min.css",
                   "~/Content/font-awesome.min.css",
                 //"~/Content/custom.min.css"
                 "~/Content/custom.css"
                 ));

            bundles.Add(new ScriptBundle("~/bundles/jshome").Include(
                "~/Scripts/jquery-3.4.1.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/swiper.min.js"
             ));

            bundles.Add(new StyleBundle("~/Content/csshome").Include(
               "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/site.css",
                //"~/Content/site.min.css",
                "~/Content/responsive.min.css",
                "~/Content/swiper.min.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
