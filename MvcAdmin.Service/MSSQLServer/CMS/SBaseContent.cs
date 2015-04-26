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
    public class SBaseContent : IBaseContent
    {
        public bool AddBaseContent(Model.MBaseContent m)
        {
            string sql01 = "insert into [cms_baseContent](name,des,content) values(@name,@des,@content)";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@name",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar),
                new SqlParameter("@content",SqlDbType.NText)
            };
            ps[0].Value = m.Name;
            ps[1].Value = m.Des;
            ps[2].Value = m.Content;

            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DelBaseContent(string ids)
        {
            string sql01 = "delete from [cms_baseContent] where id in(" + ids + ")";
            if (Core.MSSqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdBaseContent(Model.MBaseContent m)
        {
            string sql01 = "update [cms_baseContent] set name=@name,des=@des,content=@content where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@name",SqlDbType.NVarChar),
                new SqlParameter("@des",SqlDbType.NVarChar),
                new SqlParameter("@content",SqlDbType.NText)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.Name;
            ps[2].Value = m.Des;
            ps[3].Value = m.Content;
            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.MBaseContent GetBaseContent(string name)
        {
            string sql = "SELECT * FROM [cms_baseContent] where name=@name";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@name",SqlDbType.VarChar)
            };
            ps[0].Value = name;
            DataSet ds = Core.MSSqlHelper.Query(sql, ps);
            DataTable dt = ds.Tables[0];
            Model.MBaseContent m;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MBaseContent(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public Model.MBaseContent GetBaseContent(int id)
        {
            string sql = "SELECT * FROM [cms_baseContent] where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql, ps);
            DataTable dt = ds.Tables[0];
            Model.MBaseContent m;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MBaseContent(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public List<Model.MBaseContent> GetBaseContentList()
        {
            List<Model.MBaseContent> list = new List<Model.MBaseContent>();

            string sql01 = "SELECT * FROM [cms_baseContent]";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                Model.MBaseContent m = new Model.MBaseContent(r);
                list.Add(m);
            }
            return list;
        }
    }
}
