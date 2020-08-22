using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WholeWheat.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/All/css").Include
                 (
                   "~/css/normalize.min.css"
                 , "~/css/reset.min.css"
                 , "~/css/jquery-ui.css"
                 , "~/css/bootstrap.min.css"
                 , "~/css/bootstrap-horizon.css"
                 , "~/css/dataTables.bootstrap.css"
                 , "~/css/font-awesome.min.css"
                 , "~/css/summernote.css"
                 , "~/css/waves.min.css"
                 , "~/css/daterangepicker.css"
                 , "~/css/keyboard-previewkeyset.css"
                 , "~/css/keyboard.css"
                 , "~/css/select2.min.css"
                 , "~/css/sweetalert.css"
                 , "~/css/bootstrap-datepicker.min.css"
                 , "~/css/Style-Light.css"
                 ,"~/css/loader.css"
                 ));

            //style

            bundles.Add(new ScriptBundle("~/All/js").Include
                (
                "~/js/loading.js"
                , "~/js/jquery.slimscroll.min.js"
                , "~/js/waves.min.js"
                , "~/js/bootstrap.min.js"
                , "~/js/tether.min.js"
                , "~/js/jquery.keyboard.js"
                , "~/js/jquery.keyboard.extension-all.js"
                , "~/js/jquery.keyboard.extension-extender.js"
                , "~/js/jquery.keyboard.extension-typing.js"
                , "~/js/jquery.mousewheel.js"
                , "~/js/select2.min.js"
                , "~/js/jquery.dataTables.min.js"
                , "~/js/dataTables.bootstrap.js"
                , "~/js/summernote.js"
                , "~/js/moment.min.js"
                , "~/js/Notification.js"
                , "~/js/sweetalert.min.js"
                , "~/js/bootstrap-datepicker.min.js"
                , "~/js/jquery.redirect.js"
                , "~/js/app.js"
                ));
            //JS       
        }
    }
}