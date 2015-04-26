using MvcAdmin.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MySQLServer
{
    public class SModuleCategory : IModuleCategory
    {
        public List<Model.MModuleCategory> GetModuleCategoryList()
        {
            string sql01 = "SELECT * FROM `sys_modulecategory`  ";
            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            List<Model.MModuleCategory> list = new List<Model.MModuleCategory>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MModuleCategory m = new Model.MModuleCategory();
                m.Id = int.Parse(r["id"].ToString());
                m.CategoryName = r["categoryName"].ToString();
                m.Des = r["des"].ToString();
                list.Add(m);
            }
            return list;
        }

        public bool AddModuleCategory(Model.MModuleCategory m)
        {
            string sql01 = "insert into `sys_modulecategory`(categoryName,des) values(?categoryName,?des)";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?categoryName",MySqlDbType.VarChar),
                new MySqlParameter("?des",MySqlDbType.VarChar)
            };
            ps[0].Value = m.CategoryName;
            ps[1].Value = m.Des;
            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DelModuleCategory(string ids)
        {
            string sql01 = "delete from `sys_modulecategory` where id in(" + ids + ")";
            if (Core.MySqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdModuleCategory(Model.MModuleCategory m)
        {
            string sql01 = "update `sys_modulecategory` set categoryName=?categoryName,des=?des where id=?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32),
                new MySqlParameter("?categoryName",MySqlDbType.VarChar),
                new MySqlParameter("?des",MySqlDbType.VarChar)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.CategoryName;
            ps[2].Value = m.Des;
            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.MModuleCategory GetModuleCategory(int id)
        {
            string sql01 = "SELECT * FROM `sys_modulecategory` WHERE id =?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MModuleCategory m = null;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MModuleCategory(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public Model.MModuleCategory GetModuleCategory(string categoryName)
        {
            string sql01 = "SELECT * FROM `sys_modulecategory` WHERE categoryName =?categoryName";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?categoryName",MySqlDbType.VarChar)
            };
            ps[0].Value = categoryName;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MModuleCategory m = null;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MModuleCategory(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public Model.MModuleCategory GetModuleCategory(int id, string categoryName)
        {
            string sql01 = "select * from `sys_modulecategory` where categoryName=?categoryName and id not in(?id)";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?categoryName",MySqlDbType.VarChar),
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = categoryName;
            ps[1].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MModuleCategory m = null;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MModuleCategory(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }
    }
}
