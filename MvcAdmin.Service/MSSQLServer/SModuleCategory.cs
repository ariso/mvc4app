using MvcAdmin.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MSSQLServer
{
    public class SModuleCategory : IModuleCategory
    {
        public List<Model.MModuleCategory> GetModuleCategoryList()
        {
            string sql01 = "SELECT * FROM [sys_modulecategory]  ";
            DataSet ds = Core.MSSqlHelper.Query(sql01);
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
            string sql01 = "insert into [sys_modulecategory](categoryName,des) values(@categoryName,@des)";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@categoryName",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar)
            };
            ps[0].Value = m.CategoryName;
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

        public bool DelModuleCategory(string ids)
        {
            string sql01 = "delete from [sys_modulecategory] where id in(" + ids + ")";
            if (Core.MSSqlHelper.ExecuteSql(sql01) >= 1)
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
            string sql01 = "update [sys_modulecategory] set categoryName=@categoryName,des=@des where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@categoryName",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.CategoryName;
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

        public Model.MModuleCategory GetModuleCategory(int id)
        {
            string sql01 = "SELECT * FROM [sys_modulecategory] WHERE id =@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
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
            string sql01 = "SELECT * FROM [sys_modulecategory] WHERE categoryName=@categoryName";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@categoryName",SqlDbType.NVarChar)
            };
            ps[0].Value = categoryName;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
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
            string sql01 = "select * from [sys_modulecategory] where categoryName=@categoryName and id not in(@id)";
            SqlParameter[] ps = new SqlParameter[]{
                new SqlParameter("@categoryName",SqlDbType.VarChar),
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = categoryName;
            ps[1].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
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
