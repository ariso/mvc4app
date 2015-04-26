using MvcAdmin.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MSSQLServer
{
    public class SLink:ILink
    {
        public bool AddLink(Model.MLink m)
        {
            string sql01 = "insert into [cms_link](LinkName,LinkImg,LinkUrl) values(@LinkName,@LinkImg,@LinkUrl)";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@LinkName",SqlDbType.NVarChar),
                new SqlParameter("@LinkImg",SqlDbType.VarChar),
                new SqlParameter("@LinkUrl",SqlDbType.VarChar)
            };
            ps[0].Value = m.LinkName;
            ps[1].Value = m.LinkImg;
            ps[2].Value = m.LinkUrl;
            
            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
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
            string sql01 = "delete from [cms_link] where id in(" + ids + ")";
            if (Core.MSSqlHelper.ExecuteSql(sql01) >= 1)
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
            string sql01 = "update [cms_link] set LinkName=@LinkName,LinkImg=@LinkImg,LinkUrl=@LinkUrl where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@LinkName",SqlDbType.VarChar),
                new SqlParameter("@LinkImg",SqlDbType.VarChar),
                new SqlParameter("@LinkUrl",SqlDbType.VarChar)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.LinkName;
            ps[2].Value = m.LinkImg;
            ps[3].Value = m.LinkUrl;
            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
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
            string sql = "SELECT * FROM [cms_link] where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql, ps);
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

            string sql01 = "SELECT * FROM [cms_link]";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
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
