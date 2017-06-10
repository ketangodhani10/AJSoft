using System.Web;
using System.Web.Optimization;

namespace AJSoftWeb
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       "~/Scripts/jquery-ui.min.js",
                       "~/Scripts/jquery.showMessage.min.js",
                       "~/Scripts/jquery.mask.js",
                       "~/Scripts/jquery.maskedinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/plugins")
                .Include("~/Scripts/chosen.jquery.js")
                .Include("~/Scripts/Common.js")
                .Include("~/Scripts/jqGrid/jquery.jqGrid.min.js")
                .Include("~/Scripts/jqGrid/i18n/grid.locale-en.js")
                .Include("~/Scripts/jqGrid/jquery.contextmenu.js")
                .Include("~/Scripts/jquery.blockUI.js")
                .Include("~/Scripts/bootstrap-multiselect.js")
                .Include("~/Scripts/jquery.bootstrap-duallistbox.js")
                .Include("~/Scripts/moment.js")
                .Include("~/Scripts/bootstrap-datetimepicker.min.js")
                .Include("~/Scripts/dropzone.js"));

            // All integrated styles:
            bundles.Add(new StyleBundle("~/content/bundles/styles")
                .Include("~/Content/theme.css", new CssRewriteUrlTransform())
                .Include("~/Content/adminpanels.css", new CssRewriteUrlTransform())
                .Include("~/Content/admin-forms.css", new CssRewriteUrlTransform())
                .Include("~/Content/showMessage.css", new CssRewriteUrlTransform())
                .Include("~/Content/magnific-popup.css", new CssRewriteUrlTransform())
                .Include("~/Content/adminmodal.css", new CssRewriteUrlTransform())
                .Include("~/Content/ui.jqgrid.css", new CssRewriteUrlTransform())
                .Include("~/Content/chosen.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-datetimepicker.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/daterangepicker.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-colorpicker.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-multiselect.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-duallistbox.css", new CssRewriteUrlTransform())
                .Include("~/Content/dropzone.css", new CssRewriteUrlTransform())
                .Include("~/Content/tagmanager.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

        }
    }
}
