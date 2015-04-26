using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAdmin.Web.MvcHelper
{
    public class LogFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 记录内容
        /// </summary>
        public string actionContent { get; set; }
        //Ation操作完执行
        //写操作日志
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //this.actionContent可以获取数据并记录，实现操作日志
            base.OnActionExecuted(filterContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

    }
}