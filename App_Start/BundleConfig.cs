using System.Web;
using System.Web.Optimization;

namespace UstaLab
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Content/js/sb-admin-2.js",
                      "~/Content/plugins/vendor/jquery/jquery.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/plugins/vendor/fontawesome-free-6.0.0-web/css/all.min.css",                      
                      "~/Content/css/sb-admin-2.min.css",
                      "~/Content/css/sb-admin-2.css",
                      "~/Content/plugins/vendor/footable-bootstrap.latest/css/footable.bootstrap.css",
                      "~/Content/css/shLogin.css"));
        }
    }
}
