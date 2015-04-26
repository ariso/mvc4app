using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MvcAdmin
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            //引用的方法
            //@Scripts.Render("~/bundles/jquery")
            //@Styles.Render("~/Content/css")
            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
                        "~/Resources/Admin/js/jquery.min.js",
                        "~/Resources/Admin/js/jquery.form.js",//表单插件
                        "~/Resources/Admin/js/jqPaginator.js",//分页插件
                        "~/Resources/kindeditor/kindeditor.js",//kind编辑器
                        "~/Resources/kindeditor/lang/zh_CN.js",
                        "~/Resources/FancyBox/jquery.fancybox.js",//图片灯箱
                        "~/Resources/publicJs/jquery.upload.js",//文件上传插件
                        "~/Resources/ZeroClipboard/jquery.zclip.js",//文件复制插件
                        "~/Resources/Chart/highcharts.js",//图表插件
                        "~/Resources/Chart/jquery.highchartTable.js",
                        "~/Resources/Sobox/jquery.sobox.js",//弹框插件
                        "~/Resources/Admin/js/InitAndPublic.js"));//公用js
            //压缩是可以的，但是css中的图片路径会出错
            //bundles.Add(new StyleBundle("~/bundles/styles").Include(
            //            "~/Resources/Admin/css/style.css",//整体布局css
            //            "~/Resources/kindeditor/themes/default/default.css",//kind编辑器
            //            "~/Resources/FancyBox/jquery.fancybox.css",//图片灯箱
            //            "~/Resources/Sobox/style.css"));//弹框插件
            //有图片的css开启单独压缩，并且路径正确
            bundles.Add(new StyleBundle("~/Resources/Admin/css/css").Include("~/Resources/Admin/css/style.css"));
            bundles.Add(new StyleBundle("~/Resources/kindeditor/themes/default/css").Include("~/Resources/kindeditor/themes/default/default.css"));
            bundles.Add(new StyleBundle("~/Resources/FancyBox/css").Include("~/Resources/FancyBox/jquery.fancybox.css"));
            bundles.Add(new StyleBundle("~/Resources/Sobox/css").Include("~/Resources/Sobox/style.css"));

            //测试时开启压缩,默认不开启需要手动
            BundleTable.EnableOptimizations = true;
            //或者修改此处debug为false开启
            //<compilation debug="false" targetFramework="4.0">
        }
    }
}