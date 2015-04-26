using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Model;

namespace MvcAdmin.DAO
{
    /// <summary>
    /// 账户分组接口
    /// </summary>
    public interface IAccountGroup
    {
        List<MAccountGroup> GetAccountGroupList();

        MAccountGroup GetAccountGroup(int id);

        MAccountGroup GetAccountGroup(string groupName);
        /// <summary>
        /// 除了这个id的groupName，其他的有没有这个groupName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        MAccountGroup GetAccountGroup(int id,string groupName);
        /// <summary>
        /// 根据id管理添加还是更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        bool AddUpdAccountGroup(MAccountGroup m);

        bool DelAccountGroup(string ids);

    }
}
