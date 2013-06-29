using System.Web.Optimization;

namespace Dolphin.Web.WebAPI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
              new ScriptBundle("~/scripts/vendor")
                .Include("~/lib/jquery-1.9.1.min.js")
                .Include("~/lib/underscore-min.js")
                .Include("~/lib/backbone-min.js")
                .Include("~/bootstrap/js/bootstrap.js")
                .Include("~/Scripts/kendo/2013.1.319/kendo.web.min.js")
              );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/bootstrap/css/bootstrap.min.css")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/kendo/2013.1.319/kendo.common.min.css")
                .Include("~/Content/kendo/2013.1.319/kendo.default.min.css")
                .Include("~/css/styles.less")
              );
        }
    }
}