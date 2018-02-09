using System.Web;
using System.Web.Optimization;

namespace WebSolution
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryjqgrid").Include(
            "~/Scripts/jquery.jqGrid.js",
            "~/Scripts/i18n/grid.locale-tw.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-block-ui.css",
                      "~/Content/site.css",
                      "~/Content/animations.css"));

            bundles.Add(new ScriptBundle("~/Content/jqScriptBundle").Include(
                    "~/Scripts/jquery-2.2.3.js",
                    "~/Scripts/jquery-ui-1.11.4.js",
                    "~/Scripts/jquery.unobtrusive-ajax.js",
                    "~/Scripts/jquery-ui-i18n.js",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-animate.js",
                    "~/Scripts/angular-route.js",
                    "~/Scripts/angular-cookies.js",
                    "~/Scripts/angular-resource.js",
                    "~/Scripts/angular-sanitize.js",
                    "~/Scripts/angular-block-ui.js",
                    "~/Scripts/angular-tooltips.js",
                    "~/Scripts/angular-datepicker.js",
                    "~/Scripts/angular-local-storage.js",
                    "~/Scripts/ngDatepicker.js",
                    "~/Scripts/kendo/kendo.all.min.js",
                    "~/Scripts/kendo/kendo.angular.min.js",
                    "~/Scripts/ngDialog.js",
                    "~/Scripts/ngval.js",
                    "~/Scripts/angular-messages.js",
                    "~/Scripts/angular-message-format.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/angular-block-ui.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/angular-ui/ui-bootstrap.js",
                    "~/Scripts/angular-material/angular-material.js",
                    "~/Scripts/angular-aria.js"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/jquery.jqGrid/ui.jqgrid.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/KendoStyle").Include(
                    "~/Content/kendo/kendo.common.min.css",
                    "~/Content/kendo/kendo.bootstrap.min.css",
                    "~/Content/kendo/kendo.rtl.min.css",
                    "~/Content/kendo/kendo.default.min.css",
                    "~/Content/kendo/kendo.common.core.min.css",
                    "~/Content/kendo/kendo.dataviz.min.css",
                    "~/Content/kendo/kendo.dataviz.default.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/ngStyle").Include(
                    "~/Content/ngDialog/ngDialog-theme-default.css",
                    "~/Content/ngDialog/ngDialog-theme-plain.css",
                    "~/Content/ngDialog/ngDialog.css",
                    "~/Content/ngDialog/ngDialog-custom-width.css",
                    "~/Content/angular-material.css",
                    "~/Content/angular-material.layouts.css",
                    "~/Content/angular-tooltips/angular-tooltips.css",
                    "~/Content/angular-datepicker.css",
                    "~/Content/ngDatepicker.css"
                ));
        }
    }
}
