using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Model;

namespace MvcAdmin.DAO
{
    /// <summary>
    /// 账户接口
    /// </summary>
    public interface IAccount
    {
        List<MAccount> GetAccountList();

        List<Model.MAccount> GetAccountList(string keyword, int groupid, int page, int pageSize);

        int GetAccountListCount(string keyword, int groupid);

        MAccount GetAccount(string name);

        MAccount GetAccount(int id);

        bool AddAccount(MAccount m);

        bool DelAccount(string ids);

        bool UpdAccount(MAccount m);

        bool IsAccountGroup(int AccountGroup);
    }
}
