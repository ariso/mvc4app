using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Model;

namespace MvcAdmin.DAO
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IModule
    {
        List<MModule> GetModuleList();

        bool AddModule(MModule m);

        bool DelModule(string ids);
        

        bool UpdModule(MModule m);
         
        MModule GetModule(int id);

        MModule GetModule(string moduleName);

        MModule GetModule(int id,string moduleName);

        bool IsModuleCategory(int CategoryId);

    }
}
