using MvcAdmin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdmin.DAO;
using System.Web.SessionState;
using MvcAdmin.Web.MvcHelper;

namespace MvcAdmin.Web.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [ErrorFilter]
    public class HomeController : Controller
    {
        //readonly IStudentRepository repository;
        readonly IAccount account;
        //构造器注入
        public HomeController(/*IStudentRepository repository,*/ IAccount account)
        {
            //this.repository = repository;
            this.account = account;
        }
        //测试主题
        public ActionResult Index()
        {
            //var data = repository.GetAll();
            var data = account.GetAccountList();
            //return View("../Themes/{Theme}/Index", data);
            //return View("../Themes/Default/Index", data);
            return View("Index", data);
        }
        public ActionResult Index1()
        {
            //var data = repository.GetAll();
            var data = account.GetAccountList();
            //return View("../Themes/{Theme}/Index", data);
            //return View("../Themes/Default/Index", data);
            return View("Index", data);
        }
        public ActionResult Index2()
        {
            //var data = repository.GetAll();
            var data = account.GetAccountList();
            //return View("../Themes/{Theme}/Index", data);
            return View("../Themes/Red/Index", data);
            //return View("Index", data);
        }

    }
}
