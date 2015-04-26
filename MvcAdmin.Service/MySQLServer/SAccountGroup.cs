using MvcAdmin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Core;
using MySql.Data.MySqlClient;
using System.Data;

namespace MvcAdmin.Service.MySQLServer
{
    public class SAccountGroup : IAccountGroup
    {
        /// <summary>
        /// 得到账号组列表
        /// </summary>
        /// <returns></returns>
        public List<Model.MAccountGroup> GetAccountGroupList()
        {
            string sql01 = "SELECT * FROM `sys_accountgroup`";
            DataSet ds=Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            List<Model.MAccountGroup> list=new List<Model.MAccountGroup>();
            foreach(DataRow r in dt.Rows){
                Model.MAccountGroup m = new Model.MAccountGroup();
                m.Id =int.Parse( r["id"].ToString());
                m.GroupName = r["groupName"].ToString();
                m.Des = r["des"].ToString();
                list.Add(m);
            }
            return list;
        }
        /// <summary>
        /// 得到账号分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.MAccountGroup GetAccountGroup(int id)
        {
            string sql01 = "select * from `sys_accountgroup` where id=?id";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = id;
            DataSet ds=Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccountGroup m = new Model.MAccountGroup();
            m.Id =int.Parse(dt.Rows[0]["id"].ToString());
            m.GroupName = dt.Rows[0]["groupName"].ToString();
            m.Des = dt.Rows[0]["des"].ToString();
            return m;
        }

        public Model.MAccountGroup GetAccountGroup(string groupName)
        {
            string sql01 = "select * from `sys_accountgroup` where groupName=?groupName";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?groupName",MySqlDbType.VarChar)
            };
            ps[0].Value = groupName;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            Model.MAccountGroup m = new Model.MAccountGroup();
            if (dt.Rows.Count > 0)
            {
                m.Id = int.Parse(dt.Rows[0]["id"].ToString());
                m.GroupName = dt.Rows[0]["groupName"].ToString();
                m.Des = dt.Rows[0]["des"].ToString();
            }
            else {
                m = null;
            }
            return m;
        }
        /// <summary>
        /// 除了这个id的groupName，其他的有没有这个groupName
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public Model.MAccountGroup GetAccountGroup(int id, string groupName)
        {
            string sql01 = "select * from `sys_accountgroup` where groupName=?groupName and id not in(?id)";
            MySqlParameter[] ps = new MySqlParameter[]{
                new MySqlParameter("?groupName",MySqlDbType.VarChar),
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = groupName;
            ps[1].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql01, ps);
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
        /// <summary>
        /// 添加账号分组
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool AddUpdAccountGroup(Model.MAccountGroup m)
        {
            if (m.Id == 0)
            {
                string sql01 = "insert into `sys_accountgroup`(groupName,des) values(@groupName,@des)";
                MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("@groupName",MySqlDbType.VarChar),
                new MySqlParameter("@des",MySqlDbType.VarChar)
            };
                ps[0].Value = m.GroupName;
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
            else {
                string sql01 = "update `sys_accountgroup` set groupName=@groupName,des=@des where id=@id";
                MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("@id",MySqlDbType.Int32),
                new MySqlParameter("@groupName",MySqlDbType.VarChar),
                new MySqlParameter("@des",MySqlDbType.VarChar)
            };
                ps[0].Value = m.Id;
                ps[1].Value = m.GroupName;
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
        }
        /// <summary>
        /// 删除账号分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelAccountGroup(string ids)
        {
            string sql01 = "delete from `sys_accountgroup` where id in(" + ids + ")";
            if (Core.MySqlHelper.ExecuteSql(sql01) >= 1)
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
