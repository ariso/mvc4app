using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAdmin.Web.MvcHelper
{
    /// <summary>
    /// 报错回到报错页
    /// </summary>
    public class ErrorFilter:FilterAttribute, IExceptionFilter//异常报错
    {

        /*报错回到报错页Error*/
        /*
         *CreateTime：2013‎年‎3‎月‎18‎日
         */
        public void OnException(ExceptionContext filterContext)
        {
            //报错回到首页

            //IBookPageService bookpageservice = ServiceBuilder.BuildBookPageService();
            //PageModels page = new PageModels();
            //int pageshowsize = 10;//暂定，可以把这个放在cookie中
            //filterContext.Controller.ViewData["booklist"] = bookpageservice.PageALLBook(out page, pageshowsize, 1);
            //filterContext.Controller.ViewData["page"] = page;
            //filterContext.Result = new ViewResult()//new一个url为Error视图
            //{
            //    ViewName = "Index",
            //    ViewData = filterContext.Controller.ViewData//view视图的属性中的viewdata被赋值
            //};

            //报错回到报错页
            filterContext.Controller.ViewData["ErrorMessage"] = filterContext.Exception.Message+" 亲！您犯错了哦！";//得到报错的内容
            filterContext.Result = new ViewResult()//new一个url为Error视图
            {
                ViewName = "Error",/*在Shard文件夹下*/
                ViewData = filterContext.Controller.ViewData//view视图的属性中的viewdata被赋值
            };

            filterContext.ExceptionHandled = true;
        }
    }
}