using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ErdemYeniWeb.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) // nugetten indirilen paketi dahil et BundleCollection Sınıfını kullanacaz
        {
            string index_css = "~/Context/SiteContext/css/";
            string index_js = "~/Context/SiteContext/js/";
            string index_vendors = "~/Context/SiteContext/vendors/";
            string admin_css = "~/Context/AdminContext/css/";
            string admin_js = "~/Context/AdminContext/js/";
            // CSS - StyleBundle
            bundles.Add(new StyleBundle("~/css/adminlayout").Include(
                admin_css+"bootstrap.min.css",
                admin_css+"bootstrap-reset.css",
                "~/Context/AdminContext/assets/font-awesome/css/font-awesome.min.css",
                admin_css+"style.css",
                admin_css+"style-responsive.css"));

            bundles.Add(new StyleBundle("~/css/indexlayout").Include(
                index_css+"bootstrap.css",
                index_vendors + "linericon/style.css",
                index_css +"font-awesome.min.css",
                index_vendors + "owl-carousel/owl.carousel.min.css",
                index_vendors + "lightbox/simpleLightbox.css",
                index_vendors + "nice-select/css/nice-select.css",
                index_vendors + "animate-css/animate.css",
                index_vendors + "popup/magnific-popup.css",
                index_css+"style.css",
                index_css+"responsive.css"));

            // JS - ScriptBundle
            bundles.Add(new ScriptBundle("~/js/adminlayout").Include(
                admin_js + "jquery.js",
                admin_js + "jquery-1.8.3.min.js",
                admin_js + "bootstrap.min.js",
                admin_js + "jquery.scrollTo.min.js",
                admin_js + "jquery.nicescroll.js",
                admin_js + "respond.min.js",
                admin_js + "common-scripts.js"));
            bundles.Add(new ScriptBundle("~/js/indexlayout").Include(
                index_js+"jquery-3.3.1.min.js",
                index_js+"popper.js",
                index_js+"bootstrap.min.js",
                index_js+"stellar.js",
                index_vendors + "lightbox/simpleLightbox.min.js",
                index_vendors + "nice-select/js/jquery.nice-select.min.js",
                index_vendors + "isotope/imagesloaded.pkgd.min.js",
                index_vendors + "isotope/isotope.pkgd.min.js",
                index_vendors + "owl-carousel/owl.carousel.min.js",
                index_vendors + "popup/jquery.magnific-popup.min.js",
                index_js+"jquery.ajaxchimp.min.js",
                index_vendors+"counter-up/jquery.waypoints.min.js",
                index_vendors+"counter-up/jquery.counterup.min.js",
                index_js+"mail-script.js",
                index_js+"theme.js"));

            // JS - Admin - ScriptBundle
            bundles.Add(new ScriptBundle("~/js/admin/onlyJquery").Include(admin_js + "jquery-1.8.3.min.js"));
            bundles.Add(new ScriptBundle("~/js/admin/BlogDelete").Include("~/Scripts/BlogDelete.js"));
            bundles.Add(new ScriptBundle("~/js/admin/BlogComment").Include("~/Scripts/BlogComment.js"));
            bundles.Add(new ScriptBundle("~/js/admin/BlogEdit").Include("~/Scripts/BlogEdit.js"));
            bundles.Add(new ScriptBundle("~/js/admin/BlogNew").Include("~/Scripts/BlogNew.js"));
            bundles.Add(new ScriptBundle("~/js/admin/LogDelete").Include("~/Scripts/LogDelete.js"));
            bundles.Add(new ScriptBundle("~/js/admin/Tags").Include("~/Scripts/Tags.js"));
            bundles.Add(new ScriptBundle("~/js/admin/UserDetails").Include("~/Scripts/UserImage.js", "~/Scripts/UserDetail.js", "~/Scripts/UserCv.js", "~/Scripts/UserMailTest.js"));
            bundles.Add(new ScriptBundle("~/js/admin/UserInformation").Include("~/Scripts/UserInformation.js"));

            // JS - Client - ScriptBundle
            bundles.Add(new ScriptBundle("~/js/client/BlogEntry").Include("~/Scripts/ClientBlogEntry.js"));

            // Do it optimize
            BundleTable.EnableOptimizations = true; 
        }
    }
}