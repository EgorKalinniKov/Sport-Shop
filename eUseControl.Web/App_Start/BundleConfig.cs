using System.Web.Optimization;

namespace eUseControl.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {  
            //CSS
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/Styles/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/FontAwesome/css").Include(
                "~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/nouislider/css").Include(
                "~/Content/Styles/nouislider.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/slickthem/css").Include(
                "~/Content/Styles/slick-theme.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/slick/css").Include(
                "~/Content/Styles/slick.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/style/css").Include(
                "~/Content/Styles/style.css", new CssRewriteUrlTransform()));

            //Scripts
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                "~/Scripts/CustomScripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jQueary/js").Include(
                "~/Scripts/CustomScripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jQuery-Zoom/js").Include(
                "~/Scripts/CustomScripts/jquery.zoom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/main/js").Include(
                "~/Scripts/CustomScripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/nouislider/js").Include(
                "~/Scripts/CustomScripts/nouislider.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/slick/js").Include(
                "~/Scripts/CustomScripts/slick.min.js"));

        }
    }
}