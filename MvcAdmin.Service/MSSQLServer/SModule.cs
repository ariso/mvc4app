using MvcAdmin.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MSSQLServer
{
    public class SModule : IModule
    {
        public List<Model.MModule> GetModuleList()
        {
            string sql01 = "SELECT a.*,b.categoryName FROM [sys_module] a left JOIN [sys_modulecategory] b ON a.categoryId=b.id";
            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            List<Model.MModule> list = new List<Model.MModule>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MModule m = new Model.MModule(r);
                m.ModuleCategory.CategoryName = r["categoryName"].ToString();
                list.Add(m);
            }
            return list;
        }

        public bool AddModule(Model.MModule m)
        {
            string sql01 = "insert into [sys_module](moduleName,commandParameter,categoryId) values(@moduleName,@commandParameter,@categoryId)";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@moduleName",SqlDbType.NVarChar),
                new SqlParameter("@commandParameter",SqlDbType.VarChar),
                new SqlParameter("@categoryId",SqlDbType.VarChar)
            };
            ps[0].Value = m.ModuleName;
            ps[1].Value = m.CommandParameter;
            ps[2].Value = m.CategoryId;

            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DelModule(string ids)
        {
            string sql01 = "delete from [sys_module] where id in(" + ids + ")";
            SAccountGroupToModule s = new SAccountGroupToModule();
            s.DelAccountGroupToModule(ids, false);//删除不在使用的模块
            if (Core.MSSqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdModule(Model.MModule m)
        {
            string sql01 = "update [sys_module] set moduleName=@moduleName,commandParameter=@commandParameter,categoryId=@categoryId where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@moduleName",SqlDbType.NVarChar),
                new SqlParameter("@commandParameter",SqlDbType.VarChar),
                new SqlParameter("@categoryId",SqlDbType.Int)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.ModuleName;
            ps[2].Value = m.CommandParameter;
            ps[3].Value = m.CategoryId;

            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.MModule GetModule(int id)
        {
            string sql01 = "SELECT * FROM [sys_module] WHERE id =?id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("?id",SqlDbType.Int)
            };
            ps[0].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MModule m = null;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MModule(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public Model.MModule GetModule(string moduleName)
        {
            string sql01 = "SELECT * FROM [sys_module] WHERE moduleName=@moduleName";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@moduleName",SqlDbType.NVarChar)
            };
            ps[0].Value = moduleName;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MModule m = null;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MModule(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }


        public Model.MModule GetModule(int id, string moduleName)
        {
            string sql01 = "select * from [sys_module] where moduleName=@moduleName and id not in(@id)";
            SqlParameter[] ps = new SqlParameter[]{
                new SqlParameter("@moduleName",SqlDbType.NVarChar),
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = moduleName;
            ps[1].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MModule m = null;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MModule(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }


        public bool IsModuleCategory(int CategoryId)
        {
            string sql01 = "select count(*) from [sys_module] where CategoryId=@CategoryId";
            SqlParameter[] ps = new SqlParameter[]{
                new SqlParameter("@CategoryId",SqlDbType.Int)
            };
            ps[0].Value = CategoryId;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];

            int i = Convert.ToInt32(dt.Rows[0][0]);
            if (i > 0) { return true; } else { return false; }
        }


    }
}
