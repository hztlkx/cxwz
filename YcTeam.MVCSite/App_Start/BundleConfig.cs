using System.Web;
using System.Web.Optimization;

namespace YcTeam.MVCSite
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js")
            );

            //全局js
            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));

            //日历控件
            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                "~/Content/datepicker/WdatePicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/AutoComplete").Include(
                "~/Scripts/materialize/materialize.min.js",
                "~/Scripts/materialize/jquery.materialize-autocomplete.js",
                "~/Scripts/materialize/MyAutoComplete.js"));

            bundles.Add(new StyleBundle("~/Content/materialize").Include(
                "~/Content/materialize/css/materialize.min.css"));

            bundles.Add(new StyleBundle("~/Content/metro/css").Include(
                "~/Content/metro/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/metro/plugins/bootstrap/css/bootstrap.min.css",
                "~/Content/metro/css/style-metronic.css",
                "~/Content/metro/css/style.css",
                "~/Content/metro/css/style-responsive.css",
                "~/Content/metro/css/themes/green.css"));

            bundles.Add(new ScriptBundle("~/Content/metro/js").Include(
                "~/Content/metro/plugins/jquery-1.10.2.min.js",
                "~/Content/metro/scripts/app.js",
                "~/Content/metro/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                "~/Content/metro/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/metro/plugins/jquery.cookie.min.js"));
        }
    }
}
