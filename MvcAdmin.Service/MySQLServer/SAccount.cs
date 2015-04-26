using MvcAdmin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Core;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MvcAdmin.Service.MySQLServer
{
    /// <summary>
    /// 账户服务
    /// </summary>
    public class SAccount : IAccount
    {
        /// <summary>
        /// 得到账户列表,最多1000条
        /// </summary>
        /// <returns></returns>
        public List<Model.MAccount> GetAccountList()
        {
            List<Model.MAccount> list = new List<Model.MAccount>();
            list = this.GetAccountList("",0,1,1000);
            return list;
        }
        /// <summary>
        /// 关键字模糊查找账户
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Model.MAccount> GetAccountList(string keyword,int groupid,int page,int pageSize)
        {

            List<Model.MAccount> list = new List<Model.MAccount>();
            string d = "";
            if(groupid>0){
                d = "rAccountGroup=" + groupid + " and";
            }else{
                d="";
            }
            string sql01 = "SELECT a.*,b.groupName from `sys_account` a  join sys_accountgroup b on a.rAccountGroup=b.id WHERE " + d + "  accountName LIKE concat('%',?key,'%')  limit " + (page - 1) * pageSize + "," + pageSize;
            MySqlParameter[] ps = new MySqlParameter[]
            {
                new MySqlParameter ( "?key",MySqlDbType.VarChar)
            };
            ps[0].Value = keyword;
            DataSet ds = Core.MySqlHelper.Query(sql01,ps);
            DataTable dt = ds.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                Model.MAccount m = new Model.MAccount(r);
                m.AccountGrounp.GroupName = r["groupName"].ToString();
                //m.Id = int.Parse(d["id"].ToString());
                //m.AccountName = d["accountName"].ToString();
                //m.AccountPwd = d["accountPwd"].ToString();
                list.Add(m);
            }
            return list;
        }
        /// <summary>
        /// 返回总数
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int GetAccountListCount(string keyword,int groupid)
        {
            string d = "";
            if (groupid > 0)
            {
                d = "rAccountGroup=" + groupid + " and";
            }
            else
            {
                d = "";
            }
            string sql01 = "SELECT count(*) as count from `sys_account` a  join sys_accountgroup b on a.rAccountGroup=b.id WHERE " + d + " accountName LIKE concat('%',?key,'%')";
            MySqlParameter[] ps = new MySqlParameter[]
            {
                new MySqlParameter ( "?key",MySqlDbType.VarChar)
            };
            ps[0].Value = keyword;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            int count = int.Parse(dt.Rows[0]["count"].ToString());
            return count;
        }        
        

        /// <summary>
        /// 根据id查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.MAccount GetAccount(int id)
        {
            string sql01 = "SELECT * from `sys_account` WHERE id =?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccount m;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MAccount(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        /// <summary>
        /// 根据name查找
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Model.MAccount GetAccount(string name)
        {
            string sql01 = "SELECT * from `sys_account` WHERE accountName =?accountName";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?accountName",MySqlDbType.VarChar)
            };
            ps[0].Value = name;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccount m = new Model.MAccount();
            if (dt.Rows.Count > 0)
            {
                m.Id = int.Parse(dt.Rows[0]["id"].ToString());
                m.AccountName = dt.Rows[0]["accountName"].ToString();
                m.AccountPwd = dt.Rows[0]["accountPwd"].ToString();
            }
            else
            {
                m = null;
            }
            return m;
        }
        /// <summary>
        /// 增加账户
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool AddAccount(Model.MAccount m)
        {
            string sql01 = "insert into `sys_account`(accountName,accountPwd,rAccountGroup) values(?accountName,?accountPwd,?rAccountGroup)";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?accountName",MySqlDbType.VarChar),
                new MySqlParameter("?accountPwd",MySqlDbType.VarChar),
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32)
            };
            ps[0].Value = m.AccountName;
            ps[1].Value = m.AccountPwd;
            ps[2].Value = m.RAccountGroup;
            if (Core.MySqlHelper.ExecuteSql(sql01, ps)>=1) {
                return true;
            } else {
                return false;
            }
        }
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="ids">id以,分隔</param>
        /// <returns></returns>
        public bool DelAccount(string ids)
        {
            string sql01 = "delete from `sys_account` where id in(" + ids + ")";
            if (Core.MySqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更改账户
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool UpdAccount(Model.MAccount m)
        {
            string sql01 = "update `sys_account` set accountPwd=?accountPwd,rAccountGroup=?rAccountGroup where id=?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32),
                new MySqlParameter("?accountPwd",MySqlDbType.VarChar),
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32)
            };
            ps[0].Value = m.Id;
            //ps[1].Value = m.AccountName;
            ps[1].Value = m.AccountPwd;
            ps[2].Value = m.RAccountGroup;
            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsAccountGroup(int AccountGroup)
        {
            string sql01 = "select count(*) from `sys_account` where rAccountGroup=?rAccountGroup";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32)
            };
            ps[0].Value = AccountGroup;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];

            int i = Convert.ToInt32(dt.Rows[0][0]);
            if (i > 0) { return true; } else { return false; }
        }
    }
}
