using System.Web;
using System.Web.Optimization;

namespace OnlineShopProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js",
                        "~/Scripts/jquery.prettyPhoto.js",
                        "~/Scripts/jquery.scrollUp.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/price").Include(
                        "~/Scripts/price-range.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/main.js",
                        "~/Scripts/albums.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/main.js",
                        "~/Scripts/facet.js"));

            bundles.Add(new ScriptBundle("~/bundles/iesupport").Include(
                        "~/Scripts/html5shiv.js",
                        "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/prettyPhoto.css",
                "~/Content/css/price-range.css",
                "~/Content/css/animate.css",
                "~/Content/css/main.css",
                "~/Content/css/responsive.css",
                "~/Content/css/albums.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
