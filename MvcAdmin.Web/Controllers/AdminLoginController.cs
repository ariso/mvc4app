using MvcAdmin.Core;
using MvcAdmin.DAO;
using MvcAdmin.Model;
using MvcAdmin.Web.MvcHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MvcAdmin.Web.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [ErrorFilter]
    public class AdminLoginController : Controller
    {
        //
        // GET: /AdminLogin/
        readonly IAccount account;
        public AdminLoginController(IAccount account)
        {
            this.account = account;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public CodeService Code()
        {
            CodeService c = new CodeService();
            string code=c.CreateVerifyCodeImage(4, 0, true, true);//验证码数据填写
            code = DES.GetMD5String(code);
            CookieHelper.SetCookie("code", code);
            return c;
        }
        
        public ActionResult Login(string name, string pwd, string code, int role)
        {
            string error = "<script type=\"text/javascript\">alert('登陆失败');location=\"/AdminLogin/index.html\"</script>";
            if (CookieHelper.GetCookieValue("code") == DES.GetMD5String(code))
            {
                if (role == 1)/*超级管理员*/
                {
                    if (SiteConfig.GetValue("admin.name") == name)
                    {
                        if (SiteConfig.GetValue("admin.pwd") == DES.GetMD5String(pwd))
                        {
                            //存储用户名和用户ID
                            //成功
                            Authentication.SetCookieAuthentication("0", "超级管理员");
                            //去除code的Cookie
                            Response.Cookies["code"].Expires = DateTime.Now.AddYears(-100);   
                            return Redirect("/Admin/home/index");
                        }
                        else {
                            return Content(error);
                        }
                    }
                    else
                    {
                        return Content(error);
                    }
                }
                else
                {
                    MAccount m = account.GetAccount(name);
                    if (m == null)
                    {
                        return Content(error);
                    }
                    else
                    {
                        if (m.AccountPwd == DES.GetMD5String(pwd))
                        {
                            //存储用户名和用户ID
                            //成功
                            Authentication.SetCookieAuthentication(m.Id.ToString(), name);
                            //去除code的Cookie
                            Response.Cookies["code"].Expires = DateTime.Now.AddYears(-100);   
                            return Redirect("/Admin/home/index");
                        }
                        else
                        {
                            return Content(error);
                        }
                    }
                }
            }
            else {
                return Content(error);
            }
        }
    }
}
