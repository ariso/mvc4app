using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MvcAdmin.Web.MvcHelper
{
    public class Authentication
    {
        /// <summary>
        /// 设置用户登陆成功凭据（Cookie存储）
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Rights">权限</param>
        public static void SetCookieAuthentication(string name,string value)
        {
            if (true)
            {
                //数据放入ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddMinutes(30), false, value);
                //数据加密
                string enyTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enyTicket);
                cookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        public static void SetCookieAuthentication(string key, string name, string value)
        {
            if (true)
            {
                //数据放入ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddMinutes(60), false, value);
                //数据加密
                string enyTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(key, enyTicket);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取凭据中的用户数据
        /// </summary>
        /// <returns>用户数据</returns>
        public static string getCookieAuthentication(out string name)
        {
            if (isLogin())
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                name = authTicket.Name;
                string strUserData = authTicket.UserData;
                return strUserData;
            }
            else
            {
                name = "";
                return "";
            }
        }
        public static string getCookieAuthentication(out string name,string key)
        {
            if (isLogin())
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[key];
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                name = authTicket.Name;
                string strUserData = authTicket.UserData;
                return strUserData;
            }
            else
            {
                name = "";
                return "";
            }
        }
        //public static string getCookieAuthentication()
        //{
        //    if (isLogin())
        //    {
        //        string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;

        //        return strUserData;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        /// <summary>
        /// 判断用户是否登陆
        /// </summary>
        /// <returns>True,Fales</returns>
        public static bool isLogin()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        /// <summary>
        /// 注销登陆
        /// </summary>
        public static void logOut()
        {
            FormsAuthentication.SignOut();
            //HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-100);   
        }

        
    }
}