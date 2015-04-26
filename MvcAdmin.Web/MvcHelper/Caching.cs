using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Caching;
using System.Web.UI;
using System.IO;
using System.Web;


namespace MvcAdmin.Web.MvcHelper
{
    /// <summary>
    /// 老赵缓存实现
    /// </summary>
    public static class Caching
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string Cache(this HtmlHelper htmlHelper, string cacheKey, DateTime absoluteExpiration, TimeSpan slidingExpiration, Func<object> func)
        {
            return Cache(htmlHelper, cacheKey, null, absoluteExpiration, slidingExpiration, func);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheDependencies"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string Cache(this HtmlHelper htmlHelper, string cacheKey, CacheDependency cacheDependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, Func<object> func)
        {
            var cache = htmlHelper.ViewContext.HttpContext.Cache;
            var content = cache.Get(cacheKey) as string;
            
            if (content == null)
            {
                content = func().ToString();
                cache.Insert(cacheKey, content, cacheDependencies, absoluteExpiration, slidingExpiration);
            }
            return content;
        }

        public static void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key,value);
        }
        public static object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }
    }
    
}