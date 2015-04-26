using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.IO;
using System.Web.UI;
using System.Web.Routing;

namespace MvcAdmin.Web.MvcHelper
{
    /// <summary>
    /// Mvc的HtmlHelper扩展
    /// </summary>
    public static class HtmlUI
    {
        // 网站根目录。
        // 注意：如果这段代码没有运行在ASP.NET环境中，会出现异常！
        //private static readonly string s_root = HttpRuntime.AppDomainAppPath.TrimEnd('\\');
        /// <summary>
        /// 生成一个引用JS文件的HTML代码，其中URL包含了文件的最后更新时间。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MvcHtmlString RefCssFileHtml(this UrlHelper helper, string path)
        {            
            string filePath = HttpContext.Current.Server.MapPath(path);
            if (File.Exists(filePath))
            {
                path = helper.Content(path);
                string version = File.GetLastWriteTimeUtc(filePath).Ticks.ToString();
                return new MvcHtmlString(string.Format("<link type=\"text/css\" rel=\"Stylesheet\" href=\"{0}?_t={1}\" />", path, version));
            }
            else
            {
                return new MvcHtmlString("null file");
            }
        }
        /// <summary>
        /// 生成一个引用css文件的HTML代码，其中URL包含了文件的最后更新时间。
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MvcHtmlString RefJsFileHtml(this UrlHelper helper, string path)
        {
            string filePath = HttpContext.Current.Server.MapPath(path);
            if (File.Exists(filePath))
            {
                path = helper.Content(path);
                string version = File.GetLastWriteTimeUtc(filePath).Ticks.ToString();
                return new MvcHtmlString(string.Format("<script type=\"text/javascript\" src=\"{0}?_t={1}\"></script>", path, version));
            }
            else
            {
                return new MvcHtmlString("null file");
            }
        }
    }
}