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
    public class SColumn:IColumn
    {
        public Model.MColumn GetMcolumn(int id)
        {
            string sql = "SELECT * FROM [cms_column] where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int)
            };
            ps[0].Value = id;
            DataSet ds = Core.MSSqlHelper.Query(sql, ps);
            DataTable dt = ds.Tables[0];
            Model.MColumn m;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MColumn(dt.Rows[0]);
            }
            else {
                m = null;
            }
            return m;
        }

        public List<Model.MColumn> GetMcolumnList()
        {
            List<Model.MColumn> list = new List<Model.MColumn>();
            
            string sql01 = "SELECT a.*,b.`name` as pname FROM [cms_column] a LEFT JOIN [cms_column] b on a.parentId=b.id";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                Model.MColumn m = new Model.MColumn(r);
                m.Parent.Name = r["pname"].ToString();
                list.Add(m);
            }
            return list;
        }

        public List<Model.MColumn> GetMcolumnList(int parentId)
        {
            List<Model.MColumn> list = new List<Model.MColumn>();

            string sql01 = "SELECT * FROM [cms_column] WHERE parentId=@parentId";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@parentId",SqlDbType.Int)
            };
            ps[0].Value = parentId;
            DataSet ds = Core.MSSqlHelper.Query(sql01,ps);
            DataTable dt = ds.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                Model.MColumn m = new Model.MColumn(r);
                list.Add(m);
            }
            return list;
        }

        public List<Model.MColumn> GetMcolumnList(bool isNode)
        {
            List<Model.MColumn> list = new List<Model.MColumn>();

            string sql01 = "SELECT * FROM [cms_column] WHERE isNode=@isNode";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@isNode",SqlDbType.Bit)
            };
            ps[0].Value = isNode;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                Model.MColumn m = new Model.MColumn(r);
                list.Add(m);
            }
            return list;
        }

        public List<Model.MColumn> GetMcolumnList(bool isNode, int parentId)
        {
            List<Model.MColumn> list = new List<Model.MColumn>();

            string sql01 = "SELECT * FROM [cms_column] WHERE isNode=@isNode and parentId=@parentId";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@isNode",SqlDbType.Bit),
                new SqlParameter("@parentId",SqlDbType.Int)
            };
            ps[0].Value = isNode;
            ps[1].Value = parentId;
            DataSet ds = Core.MSSqlHelper.Query(sql01, ps);
            DataTable dt = ds.Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                Model.MColumn m = new Model.MColumn(r);
                list.Add(m);
            }
            return list;
        }

        public bool AddMcolumn(Model.MColumn m)
        {
            string sql01 = "insert into [cms_column](name,isNode,parentId,link) values(@name,@isNode,@parentId,@link)";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@name",SqlDbType.VarChar),
                new SqlParameter("@isNode",SqlDbType.Bit),
                new SqlParameter("@parentId",SqlDbType.Int),
                new SqlParameter("@link",SqlDbType.VarChar)
            };
            ps[0].Value = m.Name;
            ps[1].Value = m.IsNode;
            ps[2].Value = m.ParentId;
            ps[3].Value = m.Link;
            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdMcolumn(Model.MColumn m)
        {
            string sql01 = "update [cms_column] set name=@name,parentId=@parentId,isNode=@isNode,link=@link where id=@id";
            SqlParameter[] ps = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@name",SqlDbType.VarChar),
                new SqlParameter("@parentId",SqlDbType.Int),
                new SqlParameter("@isNode",SqlDbType.Bit),
                new SqlParameter("@link",SqlDbType.VarChar)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.Name;
            ps[2].Value = m.ParentId;
            ps[3].Value = m.IsNode;
            ps[4].Value = m.Link;
            if (Core.MSSqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DelMcolumn(string ids)
        {
            string sql01 = "delete from [cms_column] where id in(" + ids + ")";
            if (Core.MSSqlHelper.ExecuteSql(sql01) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public bool IsUserMcolumn(int Cid)
        {
            string sql = "SELECT count(*) FROM [cms_article] a WHERE a.rColumn in (" + Cid + ")";

            string sql01 = "SELECT COUNT(*) FROM [cms_column] a WHERE a.parentId in (" + Cid + ") and a.isNode=0001";

            DataSet ds = Core.MSSqlHelper.Query(sql);
            DataTable dt = ds.Tables[0];


            if (Convert.ToInt32(dt.Rows[0][0]) > 0)//文章中正在使用
            {
                return true;
            }
            else
            {
                DataSet ds01 = Core.MSSqlHelper.Query(sql01);//有子类
                DataTable dt01 = ds01.Tables[0];
                if (Convert.ToInt32(dt01.Rows[0][0]) > 0)
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
}
