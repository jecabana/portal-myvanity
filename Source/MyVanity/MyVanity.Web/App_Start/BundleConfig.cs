using System.Web.Optimization;

namespace MyVanity.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Uncomment this for enabling bundling and minifying
            BundleTable.EnableOptimizations = false;

            /* Style bundles */
            bundles.Add(new StyleBundle("~/Content/css/core").Include(
                      "~/Content/css/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css/patientDashboard").Include(
                      "~/Content/css/main.css",
                      "~/Content/css/templates.css",
                      "~/Content/css/lightbox.css"));

            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                      "~/Content/css/login.css"));

            bundles.Add(new ScriptBundle("~/Content/css/jquery-ui/jqueryui")
                .Include("~/Content/css/jquery-ui/jquery.ui.base.css")
                .Include("~/Content/css/jquery-ui/jquery.ui.theme.css")
                .Include("~/Content/css/jquery-ui/jquery.ui.datepicker.css"));
            
            bundles.Add(new StyleBundle("~/Content/css/ie7support").Include(
                      "~/Content/css/bootstrap-ie7.css"));

            bundles.Add(new StyleBundle("~/Content/css/management-dashboard").Include(
                       "~/Content/css/bootstrap.css").Include(
                      "~/Content/css/management-dashboard.css",
                      "~/Content/css/lightbox.css"));

            bundles.Add(new ScriptBundle("~/Content/css/lightboxcss").Include(
                        "~/Content/css/lightbox.css"));

            bundles.Add(new StyleBundle("~/Content/css/jqueryupload").Include(
                        "~/Content/css/jquery-fileupload/jquery.fileupload.css"));

            /* Script bundles */
            bundles.Add(new ScriptBundle("~/bundles/js/core").Include(
                      "~/Content/js/jquery-{version}.js",
                      "~/Content/js/knockout-{version}.js",
                      "~/Content/js/bootstrap.js",
                      "~/Content/system-js/myvanity.js",
                      "~/Content/system-js/myvanity-pager.js",
                      "~/Content/js/lightbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
                       "~/Content/js/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js/jquerydatepicker").Include(
                        "~/Content/js/jquery.ui.core.js",
                       "~/Content/js/jquery.ui.datepicker.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/js/ie8support").Include(
                      "~/Content/js/respond.js",
                      "~/Content/js/html5shiv.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/js/edit-patient-procedure").Include(
                        "~/Content/js/knockout-{version}.js",
                       "~/Content/system-js/patient-procedure.js",
                       "~/Content/js/lightbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/appointments").Include(
                       "~/Content/system-js/appointments.js",
                       "~/Content/js/lightbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/lightboxjs").Include(
                        "~/Content/js/lightbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/holder").Include(
                        "~/Content/js/holder.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/imgResizeUpload").Include(
                        "~/Content/system-js/image-uploader.js").Include(
                        "~/Content/js/jquery-fileupload/jquery.ui.widget.js").Include(
                        "~/Content/js/jquery-fileupload/load-image.min.js").Include(
                        "~/Content/js/jquery-fileupload/canvas-to-blob.min.js").Include(
                        "~/Content/js/jquery-fileupload/jquery.iframe-transport.js").Include(
                        "~/Content/js/jquery-fileupload/jquery.fileupload.js").Include(
                        "~/Content/js/jquery-fileupload/jquery.fileupload-process.js").Include(
                        "~/Content/js/jquery-fileupload/jquery.fileupload-image.js").Include(
                        "~/Content/js/jquery-fileupload/jquery.fileupload-validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/appointment-index").Include(
                        "~/Content/system-js/appointment-index.js").Include(
                        "~/Content/js/lightbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/patientDashboard").Include(
                          "~/Content/js/jquery-{version}.js",
                          "~/Content/js/knockout-{version}.js",
                          "~/Content/js/bootstrap.js",
                          "~/Content/js/jquery.validate*", 
                          "~/Content/js/jquery.ui.core.js",
                          "~/Content/js/jquery.ui.datepicker.js",
                          "~/Content/js/modernizr-{version}.js",
                          "~/Content/js/lightbox.js",
                          "~/Content/js/holder.js",
                          "~/Content/system-js/myvanity.js",
                          "~/Content/system-js/tabs.js",
                          "~/Content/system-js/patient-dashboard.js",
                          "~/Content/system-js/agents-module.js",
                          "~/Content/system-js/consent-module.js",
                          "~/Content/system-js/patient-document.js",
                          "~/Content/system-js/dashboard-appointment.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/composeMail").Include(
                        "~/Content/system-js/compose-mail.js",
                        "~/Content/js/lightbox.js"));

            bundles.Add(new ScriptBundle("~/Content/system-js/consent-edit-module").Include(
                        "~/Content/js/jquery-fileupload/jquery.ui.widget.js",
                        "~/Content/js/jquery-fileupload/jquery.fileupload.js",
                        "~/Content/js/jquery-fileupload/jquery.fileupload-process.js",
                        "~/Content/system-js/consent-edit-module.js"));

            bundles.Add(new ScriptBundle("~/bundle/js/resources-edit").Include(
                        "~/Content/system-js/resources-edit.js"));
        }
    }
}
