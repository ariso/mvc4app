using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Model;

namespace MvcAdmin.DAO
{
    /// <summary>
    /// 模块类别接口
    /// </summary>
    public interface IModuleCategory
    {
        List<MModuleCategory> GetModuleCategoryList();

        bool AddModuleCategory(MModuleCategory m);

        bool DelModuleCategory(string ids);

        bool UpdModuleCategory(MModuleCategory m);

        MModuleCategory GetModuleCategory(int id);

        MModuleCategory GetModuleCategory(string categoryName);
        /// <summary>
        /// 除该id外，是否有在用该类名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        MModuleCategory GetModuleCategory(int id,string categoryName);
    }
}
