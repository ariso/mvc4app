using MvcAdmin.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MySQLServer
{
    public class SLink:ILink
    {
        public bool AddLink(Model.MLink m)
        {
            string sql01 = "insert into `cms_link`(LinkName,LinkImg,LinkUrl) values(?LinkName,?LinkImg,?LinkUrl)";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?LinkName",MySqlDbType.VarChar),
                new MySqlParameter("?LinkImg",MySqlDbType.VarChar),
                new MySqlParameter("?LinkUrl",MySqlDbType.VarChar)
            };
            ps[0].Value = m.LinkName;
            ps[1].Value = m.LinkImg;
            ps[2].Value = m.LinkUrl;
            
            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DelLink(string ids)
        {
            string sql01 = "delete from `cms_link` where id in(" + ids + ")";
            if (Core.MySqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdLink(Model.MLink m)
        {
            string sql01 = "update `cms_link` set LinkName=?LinkName,LinkImg=?LinkImg,LinkUrl=?LinkUrl where id=?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32),
                new MySqlParameter("?LinkName",MySqlDbType.VarChar),
                new MySqlParameter("?LinkImg",MySqlDbType.VarChar),
                new MySqlParameter("?LinkUrl",MySqlDbType.VarChar)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.LinkName;
            ps[2].Value = m.LinkImg;
            ps[3].Value = m.LinkUrl;
            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.MLink GetLink(int id)
        {
            string sql = "SELECT * FROM `cms_link` where id=?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql, ps);
            DataTable dt = ds.Tables[0];
            Model.MLink m;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MLink(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public List<Model.MLink> GetLinkList()
        {
            List<Model.MLink> list = new List<Model.MLink>();

            string sql01 = "SELECT * FROM `cms_link`";

            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                Model.MLink m = new Model.MLink(r);
                list.Add(m);
            }
            return list;
        }
    }
}
