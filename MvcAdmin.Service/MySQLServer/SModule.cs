using MvcAdmin.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MySQLServer
{
    public class SModule : IModule
    {
        public List<Model.MModule> GetModuleList()
        {
            string sql01 = "SELECT a.*,b.categoryName FROM `sys_module` a left JOIN `sys_modulecategory` b ON a.categoryId=b.id";
            DataSet ds = Core.MySqlHelper.Query(sql01);
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
            string sql01 = "insert into `sys_module`(moduleName,commandParameter,categoryId) values(?moduleName,?commandParameter,?categoryId)";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?moduleName",MySqlDbType.VarChar),
                new MySqlParameter("?commandParameter",MySqlDbType.VarChar),
                new MySqlParameter("?categoryId",MySqlDbType.VarChar)
            };
            ps[0].Value = m.ModuleName;
            ps[1].Value = m.CommandParameter;
            ps[2].Value = m.CategoryId;

            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
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
            string sql01 = "update `sys_module` set moduleName=?moduleName,commandParameter=?commandParameter,categoryId=?categoryId where id=?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32),
                new MySqlParameter("?moduleName",MySqlDbType.VarChar),
                new MySqlParameter("?commandParameter",MySqlDbType.VarChar),
                new MySqlParameter("?categoryId",MySqlDbType.Int32)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.ModuleName;
            ps[2].Value = m.CommandParameter;
            ps[3].Value = m.CategoryId;

            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
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
            string sql01 = "delete from `sys_module` where id in(" + ids + ")";
            SAccountGroupToModule s = new SAccountGroupToModule();
            s.DelAccountGroupToModule(ids, false);//删除不在使用的模块
            if (Core.MySqlHelper.ExecuteSql(sql01) >= 1)
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
            string sql01 = "SELECT * FROM `sys_module` WHERE id =?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
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
            string sql01 = "SELECT * FROM `sys_module` WHERE moduleName =?moduleName";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?moduleName",MySqlDbType.VarChar)
            };
            ps[0].Value = moduleName;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MModule m = null;
            if (dt.Rows.Count > 0)
            {
                m=new Model.MModule(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        /// <summary>
        /// 除了这个id的moduleName，其他的有没有这个moduleName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public Model.MModule GetModule(int id, string moduleName)
        {
            string sql01 = "select * from `sys_module` where moduleName=?moduleName and id not in(?id)";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?moduleName",MySqlDbType.VarChar),
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = moduleName;
            ps[1].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
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

        /// <summary>
        /// 是否正在使用该分类
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public bool IsModuleCategory(int CategoryId)
        {
            string sql01 = "select count(*) from `sys_module` where CategoryId=?CategoryId";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?CategoryId",MySqlDbType.Int32)
            };
            ps[0].Value = CategoryId;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];

            int i = Convert.ToInt32(dt.Rows[0][0]);
            if (i > 0) { return true; } else { return false; }
        }



    }
}
