using MvcAdmin.DAO;
using MvcAdmin.Model;
using MvcAdmin.Web.MvcHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace MvcAdmin.Web.Areas.Admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [AdminFilter]
    [ErrorFilter]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        /*验证可以使用过滤器*/
        //初始化页面逻辑
        readonly IAccount account;
        readonly IAccountGroup accountGroup;
        readonly IModule module;
        readonly IModuleCategory moduleCategory;
        
        readonly IAccountGroupToModule accountGroupToModule;
        //构造器注入
        public HomeController(IAccount account, IAccountGroup accountGroup, IModule module, IModuleCategory moduleCategory, IAccountGroupToModule accountGroupToModule)
        {
            this.account = account;
            this.accountGroup = accountGroup;
            this.module = module;
            this.moduleCategory = moduleCategory;
            this.accountGroupToModule = accountGroupToModule;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Caching.GetCache("SiteInfo") == null)
            {
                MSite site = new MSite();
                site.SiteName = SiteConfig.GetValue("site.name");
                site.SiteDescription = SiteConfig.GetValue("site.description");
                site.SiteKeywords = SiteConfig.GetValue("site.keywords");
                site.SiteCopyright = SiteConfig.GetValue("site.copyright");
                ViewData["model"] = site;
                Caching.SetCache("SiteInfo", ViewData["model"]);
            }
            else {
                ViewData["model"] = Caching.GetCache("SiteInfo");
            }
            //管理员是0，其他大于0
            //ViewData["user"] = 0;
            string name = "";
            string value=Authentication.getCookieAuthentication(out name);
            ViewData["userID"] = name;
            ViewData["userName"]= value;
            return View();
        }
        public ActionResult Default()
        {
            return View("Default");
        }
        public ActionResult UserInfo(int id) {
            //account
            ViewData["user"] = null;
            if (id > 0)
            {
                ViewData["user"] = account.GetAccount(id);
            }
            else {
                ViewData["user"] = new MAccount() { AccountName = "超级管理员"  };
            }
            ViewData["userid"] = id;
            return View("UserInfo");
        }
        public ActionResult CacheClear()
        {
            //清理全局缓存
            List<string> keys = new List<string>();
            // retrieve application Cache enumerator
            //检索应用程序缓存计数器
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            //得到所有的键值
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            // 删除对应缓存
            for (int i = 0; i < keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(keys[i]);
            }
            return Json(new
            {
                success = true,
                msg="清理成功"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoginOut()
        {
            Authentication.logOut();
            return RedirectToRoute(new RouteValueDictionary(new
            {
                Area = "",
                controller = "AdminLogin",
                action = "index",
                id = ""
            }));
        }
        public ActionResult AlertPwd()
        {
            return View("AlertPwd");
        }

        #region 账户
        public ActionResult Account() {

            return View();
        }
        
        public ActionResult GetAccount(int id)
        {
            Model.MAccount m = account.GetAccount(id);
            ViewData["model"] = m;
            ViewData["model02"] = accountGroup.GetAccountGroupList();
            return View("AccountUpd");
        }

        public ActionResult AddAccount()
        {
            ViewData["model02"] = accountGroup.GetAccountGroupList();
            return View("AccountAdd");
        }
        #endregion

        #region  账户分组
        public ActionResult AccountGroup()
        {
            return View("AccountGroup");
        }

        public ActionResult GetAccountGroup(int id)
        {
            Model.MAccountGroup m = accountGroup.GetAccountGroup(id);
            ViewData["model"] = m;
            return View("AccountGroupUpd");
        }

        public ActionResult AddAccountGroup()
        {
            return View("AccountGroupAdd");
        }
        #endregion

        #region 模块
        public ActionResult Module()
        {
            return View("Module");
        }

        public ActionResult GetModule(int id)
        {
            Model.MModule m = module.GetModule(id);
            ViewData["model"] = m;
            ViewData["model02"] = moduleCategory.GetModuleCategoryList();
            return View("ModuleUpd");
        }

        public ActionResult AddModule()
        {
            ViewData["model02"] = moduleCategory.GetModuleCategoryList();
            return View("ModuleAdd");
        }

        public ActionResult ModuleCategory()
        {
            return View("ModuleCategory");
        }

        public ActionResult GetModuleCategory(int id)
        {
            Model.MModuleCategory m = moduleCategory.GetModuleCategory(id);
            ViewData["model"] = m;
            return View("ModuleCategoryUpd");
        }

        public ActionResult AddModuleCategory()
        {
            return View("ModuleCategoryAdd");
        }
        #endregion

        #region 账户组分配权限
        public ActionResult Power()
        {
            List<MAccountGroup> m = accountGroup.GetAccountGroupList();
            ViewData["model02"] = m;
            return View("Power");
        }
        [OutputCache(Duration = 60)]
        [ChildActionOnly]
        public ActionResult PowerView() {
            //判断是否加载，加载的数据是什么
            ViewData["isload"] = true;
            string name = "";
            string value = Authentication.getCookieAuthentication(out name);
            if (name == "0")
            {
                ViewData["model01"] = moduleCategory.GetModuleCategoryList();

                ViewData["model"] = module.GetModuleList();
            }
            else {
                //分组id
                int groupid = account.GetAccount(int.Parse(name)).RAccountGroup;
                List<MModuleCategory> list = moduleCategory.GetModuleCategoryList();
                List<MAccountGroupToModule> list02 = accountGroupToModule.GetAccountGroupToModuleList(groupid);

                List<MModuleCategory> list01 = new List<MModuleCategory>();
                
                List<MModule> list03 = new List<MModule>();
                
                //加入不同的category
                foreach (var m1 in list02)
                {
                    foreach (var m2 in list)
                    {
                        if (m1.Module.CategoryId == m2.Id)
                        {
                            bool b = true;
                            foreach (var m3 in list01)
                            {
                                if (m3.Id == m2.Id)
                                {
                                    b = false;
                                }
                            }
                            if (b)/*不存在则添加*/
                            {
                                list01.Add(m2);
                            }
                        }
                    }
                }

                foreach(var m in list02){
                    list03.Add(m.Module);
                }
                
                ViewData["model01"] = list01;

                ViewData["model"] = list03;
            }

            return View("../Shared/Power");
        }
        [OutputCache(Duration=60)]
        [ChildActionOnly]
        public ActionResult AdminPowerView()
        {
            //判断是否是管理员，然后加载
            string name = "";
            string value=Authentication.getCookieAuthentication(out name);
            if (name == "0")
            {
                ViewData["isload"] = true;
            }
            else {
                ViewData["isload"] = false;
            }
            
            return PartialView("../Shared/AdminPower");
        }

        #endregion

        #region 网站设置
        public ActionResult Site() {

            MSite site = new MSite();

            site.SiteName = SiteConfig.GetValue("site.name");
            site.SiteDescription = SiteConfig.GetValue("site.description");
            site.SiteKeywords = SiteConfig.GetValue("site.keywords");
            site.SiteCopyright = SiteConfig.GetValue("site.copyright");
            
            //ViewData["model"] = site.GetSite();
            ViewData["model"] = site;
            return View("Site");
        }

        #endregion
    }
}
