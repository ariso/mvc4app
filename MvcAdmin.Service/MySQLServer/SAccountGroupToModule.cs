using MvcAdmin.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MySQLServer
{
    public class SAccountGroupToModule : IAccountGroupToModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Model.MAccountGroupToModule> GetAccountGroupToModuleList()
        {

            string sql01 = "SELECT a.*,b.moduleName,b.commandParameter,c.categoryName,c.id as categoryId FROM `sys_accountgrouptomodule` a LEFT JOIN `sys_module` b ON a.rModule=b.id LEFT JOIN `sys_modulecategory` c on b.categoryId=c.id ";
            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            List<Model.MAccountGroupToModule> list = new List<Model.MAccountGroupToModule>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MAccountGroupToModule m = new Model.MAccountGroupToModule(r);
                //m.Id = int.Parse(r["id"].ToString());
                //m.RAccountGroup = int.Parse(r["rAccountGroup"].ToString());
                //m.RModule = int.Parse(r["rModule"].ToString());
                list.Add(m);
            }
            return list;
        }

        public List<Model.MAccountGroupToModule> GetAccountGroupToModuleList(int accountGroupId)
        {

            string sql01 = "SELECT a.*,b.moduleName,b.commandParameter,c.categoryName,c.id as categoryId FROM `sys_accountgrouptomodule` a LEFT JOIN `sys_module` b ON a.rModule=b.id LEFT JOIN `sys_modulecategory` c on b.categoryId=c.id where a.rAccountGroup=?rAccountGroup";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32)
            };
            
            ps[0].Value = accountGroupId;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            List<Model.MAccountGroupToModule> list = new List<Model.MAccountGroupToModule>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MAccountGroupToModule m = new Model.MAccountGroupToModule(r);
                //m.Id = int.Parse(r["id"].ToString());
                //m.RAccountGroup = int.Parse(r["rAccountGroup"].ToString());
                //m.RModule = int.Parse(r["rModule"].ToString());
                list.Add(m);
            }
            return list;
        }

        public List<Model.MAccountGroupToModule> GetAccountGroupToModuleList(int accountGroupId,bool isEnable)
        {

            string sql01 = "SELECT a.*,b.moduleName,b.commandParameter,c.categoryName,c.id as categoryId FROM `sys_accountgrouptomodule` a LEFT JOIN `sys_module` b ON a.rModule=b.id LEFT JOIN `sys_modulecategory` c on b.categoryId=c.id where a.rAccountGroup=?rAccountGroup and a.isEnable=?isEnable";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32),
                new MySqlParameter("?isEnable",MySqlDbType.Bit)
            };

            ps[0].Value = accountGroupId;
            ps[1].Value = isEnable;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            List<Model.MAccountGroupToModule> list = new List<Model.MAccountGroupToModule>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MAccountGroupToModule m = new Model.MAccountGroupToModule(r);
                //m.Id = int.Parse(r["id"].ToString());
                //m.RAccountGroup = int.Parse(r["rAccountGroup"].ToString());
                //m.RModule = int.Parse(r["rModule"].ToString());
                list.Add(m);
            }
            return list;
        }

        public Model.MAccountGroupToModule GetAccountGroupToModule(int id)
        {
            string sql01 = "select * from `sys_accountgrouptomodule` where id=?id";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccountGroupToModule m = new Model.MAccountGroupToModule();
            if (dt.Rows.Count > 0)
            {
                //m.Id = int.Parse(dt.Rows[0]["id"].ToString());
                //m.RAccountGroup = int.Parse(dt.Rows[0]["rAccountGroup"].ToString());
                //m.RModule = int.Parse(dt.Rows[0]["rModule"].ToString());
                m = new Model.MAccountGroupToModule(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public Model.MAccountGroupToModule GetAccountGroupToModule(int rAccountGroup, int Module)
        {
            string sql01 = "select * from `sys_accountgrouptomodule` where rAccountGroup=?rAccountGroup and rModule=?rModule";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32),
                new MySqlParameter("?rModule",MySqlDbType.Int32)
            };
            ps[0].Value = rAccountGroup;
            ps[1].Value = Module;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccountGroupToModule m = new Model.MAccountGroupToModule();
            if (dt.Rows.Count > 0)
            {
                //m.Id = int.Parse(dt.Rows[0]["id"].ToString());
                //m.RAccountGroup = int.Parse(dt.Rows[0]["rAccountGroup"].ToString());
                //m.RModule = int.Parse(dt.Rows[0]["rModule"].ToString());
                m = new Model.MAccountGroupToModule(dt.Rows[0]);
            }
            else {
                m = null;
            }
            return m;
        }

        public bool IsUserModule(int ModuleId,bool isEnable)
        {
            string sql = "SELECT COUNT(*) FROM `sys_accountgrouptomodule` a WHERE a.rModule=?Module and a.isEnable=?isEnable ";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?Module",MySqlDbType.Int32),
                new MySqlParameter("?isEnable",MySqlDbType.Bit)
            };
            ps[0].Value = ModuleId;
            ps[1].Value = isEnable;

            DataSet ds = Core.MySqlHelper.Query(sql, ps);
            DataTable dt = ds.Tables[0];
            int i = Convert.ToInt32(dt.Rows[0][0]);
            if (i >0) { return true; } else { return false; }
        }

        public bool AddUpdAccountGroupToModule(Model.MAccountGroupToModule m)
        {
            if (m.Id == 0)
            {
                string sql01 = "insert into `sys_accountgrouptomodule`(rAccountGroup,rModule,isEnable) values(?rAccountGroup,?rModule,?isEnable)";
                MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32),
                new MySqlParameter("?rModule",MySqlDbType.Int32),
                new MySqlParameter("?isEnable",MySqlDbType.Bit)
            };
                ps[0].Value = m.RAccountGroup;
                ps[1].Value = m.RModule;
                ps[2].Value = m.IsEnable;
                if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
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
                string sql01 = "update `sys_accountgrouptomodule` set rAccountGroup=?rAccountGroup,rModule=?rModule,isEnable=?isEnable where id=?id";
                MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32),
                new MySqlParameter("?rAccountGroup",MySqlDbType.Int32),
                new MySqlParameter("?rModule",MySqlDbType.Int32),
                new MySqlParameter("?isEnable",MySqlDbType.Bit)
            };
                ps[0].Value = m.Id;
                ps[1].Value = m.RAccountGroup;
                ps[2].Value = m.RModule;
                ps[3].Value = m.IsEnable;
                if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DelAccountGroupToModule(string ids)
        {
            string sql01 = "delete from `sys_accountgrouptomodule` where id in(" + ids + ")";
            if (Core.MySqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DelAccountGroupToModule(string ModuleIds, bool isEnable)
        {
            string sql01 = "delete from `sys_accountgrouptomodule` where rModule in(" + ModuleIds + ") and isEnable=?isEnable";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?isEnable",MySqlDbType.Bit)
            };
            ps[0].Value = isEnable;
            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //public List<Model.MAccountGroupToModule> GetAccountGroupToModuleList(int accountGroupId)
        //{
        //    string sql01 = "SELECT * FROM `sys_accountgrouptomodule` where rAccountGroup=?rAccountGroup";
        //    MySqlParameter[] ps = new MySqlParameter[]{
        //        new MySqlParameter("?rAccountGroup",MySqlDbType.Int32)
        //    };
        //    ps[0].Value = accountGroupId;
        //    DataSet ds = Core.MySqlHelper.Query(sql01,ps);
        //    DataTable dt = ds.Tables[0];
        //    List<Model.MAccountGroupToModule> list = new List<Model.MAccountGroupToModule>();
        //    foreach (DataRow r in dt.Rows)
        //    {
        //        Model.MAccountGroupToModule m = new Model.MAccountGroupToModule(r);
        //        //m.Id = int.Parse(r["id"].ToString());
        //        //m.RAccountGroup = int.Parse(r["rAccountGroup"].ToString());
        //        //m.RModule = int.Parse(r["rModule"].ToString());
        //        list.Add(m);
        //    }
        //    return list;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rAccountGroup">账号组id</param>
        /// <param name="rModules">模块id字符串逗号分割</param>
        /// <returns></returns>
        public bool AddUpdAccountGroupToModule(int rAccountGroup, string rModules)
        {
            try
            {
                string[] ids = rModules.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    Model.MAccountGroupToModule m = null;
                    string[] cmds = ids[i].Split(':');
                    m = this.GetAccountGroupToModule(rAccountGroup, int.Parse(cmds[0]));                                        
                    if (m == null)
                    {
                        m = new Model.MAccountGroupToModule();
                        m.Id = 0;
                        m.RAccountGroup = rAccountGroup;
                        m.RModule = int.Parse(cmds[0]);
                        m.IsEnable = Convert.ToBoolean(cmds[1]);
                        this.AddUpdAccountGroupToModule(m);
                    }
                    else
                    {
                        m.IsEnable = Convert.ToBoolean(cmds[1]);
                        this.AddUpdAccountGroupToModule(m);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        //public bool AddUpdAccountGroupToModule(int rAccountGroup, string rModules)
        //{
        //    try
        //    {
        //        string[] ids = rModules.Split(',');
        //        for (int i = 0; i < ids.Length; i++)
        //        {
        //            Model.MAccountGroupToModule m = null;
        //            m = this.GetAccountGroupToModule(rAccountGroup, int.Parse(ids[i]));
        //            if (m == null)
        //            {
        //                m = new Model.MAccountGroupToModule();
        //                m.Id = 0;
        //                m.RAccountGroup = rAccountGroup;
        //                m.RModule = int.Parse(ids[i]);
        //                m.IsEnable = true;
        //                this.AddUpdAccountGroupToModule(m);
        //            }
        //            else
        //            {
        //                this.AddUpdAccountGroupToModule(m);
        //            }
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}

    }
}
