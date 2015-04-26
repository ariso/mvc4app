using MvcAdmin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAdmin.DAO
{
    /// <summary>
    /// 账户分组与模块关联接口
    /// </summary>
    public interface IAccountGroupToModule
    {
        List<MAccountGroupToModule> GetAccountGroupToModuleList();
        List<MAccountGroupToModule> GetAccountGroupToModuleList(int accountGroupId);
        List<MAccountGroupToModule> GetAccountGroupToModuleList(int accountGroupId, bool isEnable);

        bool IsUserModule(int ModuleId, bool isEnable);

        MAccountGroupToModule GetAccountGroupToModule(int id);
        
        MAccountGroupToModule GetAccountGroupToModule(int rAccountGroup, int Module);
        /// <summary>
        /// 根据id管理添加还是更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        bool AddUpdAccountGroupToModule(MAccountGroupToModule m);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAccountGroup">账号组id</param>
        /// <param name="rModules">模块id字符串逗号分割</param>
        /// <returns></returns>
        bool AddUpdAccountGroupToModule(int rAccountGroup, string rModules);

        bool DelAccountGroupToModule(string ids);

        bool DelAccountGroupToModule(string ModuleIds, bool isEnable);
    }
}
