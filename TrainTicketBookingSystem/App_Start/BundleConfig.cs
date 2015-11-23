using System.Web.Optimization;

namespace TrainTicketBookingSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/bower_components/jquery/dist/jquery.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/bower_components/jquery.validate/jquery.validate.js",
                "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/bower_components/bootstrap/dist/js/bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
                "~/bower_components/moment/min/moment-with-locales.min.js",
                "~/bower_components/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/App/Search/result.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/bower_components/bootswatch-dist/css/bootstrap.min.css",
                      "~/bower_components/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css",
                      "~/Content/site.css"
            ));
        }
    }
}
