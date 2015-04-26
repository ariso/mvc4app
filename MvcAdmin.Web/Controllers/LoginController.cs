using MvcAdmin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdmin.DAO;
using System.Web.SessionState;

namespace MvcAdmin.Web.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        readonly IAccount account;
        public LoginController(IAccount account)
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
            CookieHelper.SetCookie("code", code);

            return c;
        }
        public ActionResult Login(string name, string pwd, string code, int role)
        {
            if (CookieHelper.GetCookieValue("code")==code) {
                if (role == 1)/*超级管理员*/
                {

                }
                else { 
                
                }
                
            }
            return Redirect("/Admin/home/index");
        }
    }
}
