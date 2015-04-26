using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcAdmin.Web.MvcHelper
{
    public class AdminFilter : FilterAttribute, IActionFilter
    {
       
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }
        //在处理action前对身份进行验证，做出判断
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Authentication.isLogin())
            {
                //判断用户
                //用户信息缓存
                string name="";
                string value = Authentication.getCookieAuthentication(out name);
                if (string.IsNullOrEmpty(name)) {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        Area = "",
                        controller = "AdminLogin",
                        action = "index",
                        id = ""
                    }));//登陆界面
                    return;
                }

            }
            else
            {
                /*
                 *ViewResult继承至抽象类ViewResultBase
                 *ViewResultBase继承至抽象类ActionResult
                 */
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Area = "",
                    controller = "AdminLogin",
                    action = "index",
                    id = ""
                }));//登陆界面

            }

        }
    }
}