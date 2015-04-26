using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.DAO;
using System.Data.SqlClient;
using System.Data;

namespace MvcAdmin.Service.MSSQLServer
{
    public class SAccount:IAccount
    {

        public List<Model.MAccount> GetAccountList()
        {
            List<Model.MAccount> list = new List<Model.MAccount>();
            list = this.GetAccountList("", 0, 1, 1000);
            return list;
        }

        public List<Model.MAccount> GetAccountList(string keyword, int groupid, int page, int pageSize)
        {
            List<Model.MAccount> list = new List<Model.MAccount>();
            string d = "";
            if (groupid > 0)
            {
                d = "c.rAccountGroup=" + groupid + " and";
            }
            else
            {
                d = "";
            }
            //string sql01 = "SELECT a.*,b.groupName from [sys_account] a  join sys_accountgroup b on a.rAccountGroup=b.id WHERE " + d + "  accountName LIKE '%'+@key+'%'  limit " + (page - 1) * pageSize + "," + pageSize;
            string sql01 = "SELECT a.*,b.groupName from [sys_account] a  join sys_accountgroup b on a.rAccountGroup=b.id where a.id in (SELECT top " + pageSize + " e.id FROM ( SELECT top " + page * pageSize + " c.id from [sys_account] c " +
            "join sys_accountgroup d on c.rAccountGroup=d.id WHERE " + d + " c.accountName LIKE '%'+@key+'%')as e)";
            
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter ( "@key",SqlDbType.VarChar)
            };
            ps[0].Value = keyword;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
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
        public int GetAccountListCount(string keyword, int groupid)
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
            string sql01 = "SELECT count(*) as count from [sys_account] a  join sys_accountgroup b on a.rAccountGroup=b.id WHERE " + d + " accountName LIKE '%'+@key+'%'";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter ( "@key",SqlDbType.VarChar)
            };
            ps[0].Value = keyword;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
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
            string sql01 = "SELECT * from [sys_account] WHERE id =@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
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
            string sql01 = "SELECT * from [sys_account] WHERE accountName =@accountName";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@accountName",SqlDbType.VarChar)
            };
            ps[0].Value = name;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
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
            string sql01 = "insert into [sys_account](accountName,accountPwd,rAccountGroup) values(@accountName,@accountPwd,@rAccountGroup)";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@accountName",SqlDbType.VarChar),
                new SqlParameter("@accountPwd",SqlDbType.VarChar),
                new SqlParameter("@rAccountGroup",SqlDbType.Int)
            };
            ps[0].Value = m.AccountName;
            ps[1].Value = m.AccountPwd;
            ps[2].Value = m.RAccountGroup;
            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
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
            string sql01 = "delete from [sys_account] where id in(" + ids + ")";
            if (Core.MSSqlHelper.ExecuteSql(sql01) >= 1)
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
            string sql01 = "update [sys_account] set accountPwd=@accountPwd,rAccountGroup=@rAccountGroup where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@accountPwd",SqlDbType.VarChar),
                new SqlParameter("@rAccountGroup",SqlDbType.Int)
            };
            ps[0].Value = m.Id;
            //ps[1].Value = m.AccountName;
            ps[1].Value = m.AccountPwd;
            ps[2].Value = m.RAccountGroup;
            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
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
            string sql01 = "select count(*) from [sys_account] where rAccountGroup=@rAccountGroup";
            SqlParameter[] ps = new SqlParameter[]{
                new SqlParameter("@rAccountGroup",SqlDbType.Int)
            };
            ps[0].Value = AccountGroup;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];

            int i = Convert.ToInt32(dt.Rows[0][0]);
            if (i > 0) { return true; } else { return false; }
        }
    }
}
