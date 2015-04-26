using MvcAdmin.Core;
using MvcAdmin.DAO;
using MvcAdmin.Model;
using MvcAdmin.Web.MvcHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MvcAdmin.Web.Areas.Admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [AdminFilter]
    [ErrorFilter]
    public class HandleController : Controller
    {
        readonly IAccount account;
        readonly IAccountGroup accountGroup;
        readonly IModule module;
        readonly IModuleCategory moduleCategory;
        readonly IAccountGroupToModule accountGroupToModule;
        
        //构造器注入
        public HandleController(IAccount account, IAccountGroup accountGroup, IModule module, IModuleCategory moduleCategory,IAccountGroupToModule accountGroupToModule)
        {
            this.account = account;
            this.accountGroup = accountGroup;
            this.module = module;
            this.moduleCategory = moduleCategory;
            this.accountGroupToModule = accountGroupToModule;
            
        }

        #region Account
        public ActionResult AccountList(string key,int groupid,int page)
        {
            return Json(new
            {
                success = true,
                rows = account.GetAccountList(key, groupid, page, 10),
                count = account.GetAccountListCount(key,groupid)
            });
            //return Json(new { success = true, msg = "lww ok", rows = new List<Model.MAccount>
            //    { 
            //        new Model.MAccount(){ Id=1, AccountName="lww"} 
            //    },count=1 });
        }

        public ActionResult GetAccount(int id) {
            Model.MAccount m=account.GetAccount(id);
            return Json(new
            {
                id=m.Id,
                name=m.AccountName
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountName(string name)
        {
            Model.MAccount m = account.GetAccount(name);
            if (m==null)
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddAccount(string name,int groupid, string pwd)
        {
            Model.MAccount m = account.GetAccount(name);
            if (m != null)
            {
                return Json(new
                {
                    success = false,
                    msg="该用户已经存在"
                }, JsonRequestBehavior.AllowGet);
            }
            if (account.AddAccount(new Model.MAccount() { AccountName = name, AccountPwd = DES.GetMD5String(pwd), RAccountGroup = groupid }))
            {
                return Json(new
                {
                    success = true
                },JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new
                {
                    success = false,
                    msg="系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdAccount(int id, int groupid, string pwd)
        {
            if (account.UpdAccount(new Model.MAccount() { Id = id, AccountPwd = DES.GetMD5String(pwd), RAccountGroup = groupid }))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelAccount(string ids)
        {
            if (account.DelAccount(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AccountGroupList()
        {
            return Json(new
            {
                success = true,
                rows = accountGroup.GetAccountGroupList()
            });
            //return Json(new { success = true, msg = "lww ok", rows = new List<Model.MAccount>
            //    { 
            //        new Model.MAccount(){ Id=1, AccountName="lww"} 
            //    },count=1 });
        }

        public ActionResult GetAccountGroupName(string groupName)
        {
            Model.MAccountGroup m = accountGroup.GetAccountGroup(groupName);
            if (m == null)
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddAccountGroup(string groupName, string des)
        {
            Model.MAccountGroup m = accountGroup.GetAccountGroup(groupName);
            
            if (m != null)
            {
                return Json(new
                {
                    success = false,
                    msg = "该分组已经存在"
                }, JsonRequestBehavior.AllowGet);
            }
            if (accountGroup.AddUpdAccountGroup(new MAccountGroup() { GroupName = groupName, Des = des }))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdAccountGroup(int id,string groupName, string des)
        {
            MAccountGroup m = new MAccountGroup();
            m.Id = id;
            m.GroupName = groupName;
            m.Des = des;
            
            Model.MAccountGroup mm = accountGroup.GetAccountGroup(m.Id, m.GroupName);

            if (mm != null)
            {
                return Json(new
                {
                    success = false,
                    msg = "该分组已经存在"
                }, JsonRequestBehavior.AllowGet);
            }


            if (accountGroup.AddUpdAccountGroup(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //
        public ActionResult DelAccountGroup(string ids)
        {
            //判断模块是否在使用
            string[] arr = ids.Split(',');
            bool b = false;
            foreach (var s in arr)
            {
                if (account.IsAccountGroup(int.Parse(s)))
                {
                    b = true;
                    continue;
                }
            }
            if (b)
            {
                return Json(new
                {
                    success = false,
                    msg = "该账户分组正在使用，无法删除"
                }, JsonRequestBehavior.AllowGet);
            }

            if (accountGroup.DelAccountGroup(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Module

        public ActionResult ModuleList() {

            var list = module.GetModuleList();
            return Json(new
            {
                success = true,
                rows = list
                //count = list.Count
            });
        }

        public ActionResult AddModule(string moduleName, string commandParameter, int CategoryId)
        {
            Model.MModule m = module.GetModule(moduleName);
            if (m != null)
            {
                return Json(new
                {
                    success = false,
                    msg = "该模块已经存在"
                }, JsonRequestBehavior.AllowGet);
            }
            if (module.AddModule(new Model.MModule() { ModuleName = moduleName, CommandParameter = commandParameter, CategoryId = CategoryId }))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdModule(int id, string ModuleName, string CommandParameter, int CategoryId)
        {
            Model.MModule m = new MModule();
            m.Id=id;
            m.ModuleName = ModuleName;
            m.CommandParameter = CommandParameter;
            m.CategoryId = CategoryId;

            Model.MModule mm = module.GetModule(m.Id, m.ModuleName);

            if (mm != null)
            {
                return Json(new
                {
                    success = false,
                    msg = "该模块名已经存在"
                }, JsonRequestBehavior.AllowGet);
            }


            if (module.UpdModule(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetModule(int id)
        {
            Model.MModule m = module.GetModule(id);
            return Json(new
            {
                id = m.Id,
                moduleName = m.ModuleName,
                caretoryId = m.CategoryId,
                commandParameter = m.CommandParameter
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModuleName(string ModuleName)
        {
            Model.MModule m = module.GetModule(ModuleName);
            if (m == null)
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //
        public ActionResult DelModule(string ids) {
            //判断模块是否在使用
            string[] arr=ids.Split(',');
            bool b = false;
            foreach(var s in arr){
                if (accountGroupToModule.IsUserModule(int.Parse(s), true))
                {
                    b = true;
                    continue;
                }
            }
            if (b) {
                return Json(new
                {
                    success = false,
                    msg="该模块正在使用，无法删除"
                }, JsonRequestBehavior.AllowGet);
            }

            if (module.DelModule(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "删除失败"
                }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ModuleCategoryList()
        {
            var list = moduleCategory.GetModuleCategoryList();
            return Json(new
            {
                success = true,
                rows = list
            });
        }
        
        public ActionResult AddModuleCategory(string categoryName, string des)
        {

            Model.MModuleCategory m = moduleCategory.GetModuleCategory(categoryName);
            if (m != null)
            {
                return Json(new
                {
                    success = false,
                    msg = "该分类已经存在"
                }, JsonRequestBehavior.AllowGet);
            }
            if (moduleCategory.AddModuleCategory(new Model.MModuleCategory() { CategoryName = categoryName, Des = des }))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }

        }
        
        public ActionResult UpdModuleCategory(int id,string CategoryName,string Des)
        {
            Model.MModuleCategory m = new MModuleCategory();
            m.Id=id;
            m.CategoryName = CategoryName;
            m.Des = Des;

            Model.MModuleCategory mm = moduleCategory.GetModuleCategory(m.Id, m.CategoryName);

            if (mm != null)
            {
                return Json(new
                {
                    success = false,
                    msg = "该模块分类名已经存在"
                }, JsonRequestBehavior.AllowGet);
            }

            if (moduleCategory.UpdModuleCategory(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetModuleCategory(int id)
        {
            Model.MModuleCategory m = moduleCategory.GetModuleCategory(id);
            return Json(new
            {
                id = m.Id,
                categoryName = m.CategoryName,
                des = m.Des
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModuleCategoryName(string categoryName)
        {
            Model.MModuleCategory m = moduleCategory.GetModuleCategory(categoryName);
            if (m == null)
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //
        public ActionResult DelModuleCategory(string ids) {
            //判断是否有模块在使用该模块分类
            string[] arr = ids.Split(',');
            bool b = false;
            foreach (var s in arr)
            {
                if (module.IsModuleCategory(int.Parse(s)))
                {
                    b = true;
                    continue;
                }
            }
            if (b)
            {
                return Json(new
                {
                    success = false,
                    msg = "该模块分类正在使用，无法删除"
                }, JsonRequestBehavior.AllowGet);
            }

            if (moduleCategory.DelModuleCategory(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region power
        public ActionResult SavePower(int id,string ids)
        {
            if (accountGroupToModule.AddUpdAccountGroupToModule(id, ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
            //return View("Power");
        }

        public ActionResult GetPower(int id)
        {
            var list = accountGroupToModule.GetAccountGroupToModuleList(id,true);
            
            return Json(new
            {
                success = true,
                rows = list
                //count = list.Count
            });
        }

        public ActionResult PowerList()
        {
            var list = module.GetModuleList();
            
            return Json(new
            {
                success = true,
                rows = list
                //count = list.Count
            });
        }
        #endregion


        public ActionResult SaveSite(string SiteId, string SiteName, string SiteDescription, string SiteKeywords, string SiteCopyright)
        {
            MSite m = new MSite();
            m.SiteId = SiteId;
            m.SiteName = SiteName;
            m.SiteDescription = SiteDescription;
            m.SiteKeywords = SiteKeywords;
            m.SiteCopyright = SiteCopyright;
            
            SiteConfig.UpdValue("site.name",m.SiteName);
            SiteConfig.UpdValue("site.description",m.SiteDescription);
            SiteConfig.UpdValue("site.keywords",m.SiteKeywords );
            SiteConfig.UpdValue("site.copyright",m.SiteCopyright);
            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
            //if(site.SaveSite(m))
            //{
            //    return Json(new
            //    {
            //        success = true
            //    }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new
            //    {
            //        success = false,
            //        msg = "系统错误"
            //    }, JsonRequestBehavior.AllowGet);
            //}
        }


        public ActionResult AlertPwd(string oldPwd,string newPwd) {
            //取得用户信息判断是否超级管理员
            int uid = 0;
            if (uid == 0)
            {
                string pwd=SiteConfig.GetValue("admin.pwd");
                oldPwd = DES.GetMD5String(oldPwd);
                if (pwd == oldPwd)
                {
                    SiteConfig.UpdValue("admin.pwd", DES.GetMD5String(newPwd));
                    return Json(new
                    {
                        success = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        success = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else {
                MAccount m = account.GetAccount(uid);
                if (DES.GetMD5String(oldPwd) == DES.GetMD5String(m.AccountPwd))
                {
                    m.AccountPwd = DES.GetMD5String(newPwd);
                    if (account.UpdAccount(m))
                    {

                        return Json(new
                            {
                                success = true
                            }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return Json(new
                        {
                            success = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                        {
                            success = false
                        }, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }
}
