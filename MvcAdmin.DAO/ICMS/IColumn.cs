using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Model;

namespace MvcAdmin.DAO
{
    public interface IColumn
    {
        MColumn GetMcolumn(int id);
        List<MColumn> GetMcolumnList();
        /// <summary>
        /// 该父节点下的所有节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<MColumn> GetMcolumnList(int parentId);
        /// <summary>
        /// 所有节点或者不是节点
        /// </summary>
        /// <param name="isNode"></param>
        /// <returns></returns>
        List<MColumn> GetMcolumnList(bool isNode);

        List<Model.MColumn> GetMcolumnList(bool isNode, int parentId);
        bool AddMcolumn(MColumn m);
        bool UpdMcolumn(MColumn m);
        bool DelMcolumn(string ids);

        bool IsUserMcolumn(int Cid);
    }
}
