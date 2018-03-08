using System.Web;
using System.Web.Optimization;

namespace RaepieHomePage
{
    public class BundleConfig
    {
        // 묶음에 대한 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=301862를 참조하세요.
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Modernizr의 개발 버전을 사용하여 개발하고 배우십시오. 그런 다음
            // 프로덕션에 사용할 준비를 하고 https://modernizr.com의 빌드 도구를 사용하여 필요한 테스트만 선택하세요.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/raepiehome/css").Include(
                "~/Template/assets/vendor/bootstrap/css/bootstrap.min.css",
                "~/Template/assets/vendor/fontawesome/css/font-awesome.min.css",
                "~/Template/assets/vendor/flaticons/flaticon.css",
                "~/Template/assets/vendor/hover/css/hover-min.css",
                "~/Template/assets/vendor/wow/animate.css" ,
                "~/Template/assets/vendor/mfp/css/magnific-popup.css"
                ));

            bundles.Add(new ScriptBundle("~/raepiehome/js").Include(
                "~/Template/assets/vendor/jquery/js/jquery-2.2.0.min.js",
                "~/Template/assets/vendor/bootstrap/js/bootstrap.min.js", 
                "~/Template/assets/vendor/imagesloaded/js/imagesloaded.pkgd.min.js",
                "~/Template/assets/vendor/isotope/js/isotope.pkgd.min.js", 
                "~/Template/assets/vendor/mfp/js/jquery.magnific-popup.min.js",
                "~/Template/assets/vendor/circle-progress/circle-progress.js",
                "~/Template/assets/vendor/waypoints/waypoints.min.js",
                "~/Template/assets/vendor/anicounter/jquery.counterup.min.js",
                "~/Template/assets/vendor/wow/wow.min.js",
                "~/Template/assets/vendor/pjax/jquery.pjax.js",
                "~/Template/assets/vendor/footable/footable.all.min.js",
                "~/Template/assets/custom/js/custom.js"
                ));
        }
    }
}
