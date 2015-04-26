using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.DAO;
using System.Data;
using System.Data.SqlClient;

namespace MvcAdmin.Service.MSSQLServer
{
    public class SAccountGroup : IAccountGroup
    {

        public List<Model.MAccountGroup> GetAccountGroupList()
        {
            string sql01 = "SELECT * FROM [sys_accountgroup]";
            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            List<Model.MAccountGroup> list = new List<Model.MAccountGroup>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MAccountGroup m = new Model.MAccountGroup();
                m.Id = int.Parse(r["id"].ToString());
                m.GroupName = r["groupName"].ToString();
                m.Des = r["des"].ToString();
                list.Add(m);
            }
            return list;
        }

        public Model.MAccountGroup GetAccountGroup(int id)
        {
            string sql01 = "select * from [sys_accountgroup] where id=@id";
            SqlParameter[] ps = new SqlParameter[]{
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccountGroup m = new Model.MAccountGroup();
            m.Id = int.Parse(dt.Rows[0]["id"].ToString());
            m.GroupName = dt.Rows[0]["groupName"].ToString();
            m.Des = dt.Rows[0]["des"].ToString();
            return m;
        }

        public Model.MAccountGroup GetAccountGroup(string groupName)
        {
            string sql01 = "select * from [sys_accountgroup] where groupName=@groupName";
            SqlParameter[] ps = new SqlParameter[]{
                new SqlParameter("@groupName",SqlDbType.NVarChar)
            };
            ps[0].Value = groupName;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccountGroup m = new Model.MAccountGroup();
            if (dt.Rows.Count > 0)
            {
                m.Id = int.Parse(dt.Rows[0]["id"].ToString());
                m.GroupName = dt.Rows[0]["groupName"].ToString();
                m.Des = dt.Rows[0]["des"].ToString();
            }
            else
            {
                m = null;
            }
            return m;
        }

        public Model.MAccountGroup GetAccountGroup(int id, string groupName)
        {
            string sql01 = "select * from [sys_accountgroup] where groupName=@groupName and id not in(@id)";
            SqlParameter[] ps = new SqlParameter[]{
                new SqlParameter("@groupName",SqlDbType.NVarChar),
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = groupName;
            ps[1].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccountGroup m = new Model.MAccountGroup();
            if (dt.Rows.Count > 0)
            {
                m.Id = int.Parse(dt.Rows[0]["id"].ToString());
                m.GroupName = dt.Rows[0]["groupName"].ToString();
                m.Des = dt.Rows[0]["des"].ToString();
            }
            else
            {
                m = null;
            }
            return m;
        }

        public bool AddUpdAccountGroup(Model.MAccountGroup m)
        {
            if (m.Id == 0)
            {
                string sql01 = "insert into [sys_accountgroup](groupName,des) values(@groupName,@des)";
                SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@groupName",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar)
            };
                ps[0].Value = m.GroupName;
                ps[1].Value = m.Des;
                if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string sql01 = "update [sys_accountgroup] set groupName=@groupName,des=@des where id=@id";
                SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@groupName",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar)
            };
                ps[0].Value = m.Id;
                ps[1].Value = m.GroupName;
                ps[2].Value = m.Des;
                if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DelAccountGroup(string ids)
        {
            string sql01 = "delete from [sys_accountgroup] where id in(" + ids + ")";
            if (Core.MSSqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
