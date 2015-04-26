using MvcAdmin.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MySQLServer
{
    public class SArticle : IArticle
    {
        public Model.MArticle GetArticle(int id)
        {
            string sql = "SELECT * FROM `cms_article` where id=?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32)
            };
            ps[0].Value = id;
            DataSet ds = Core.MySqlHelper.Query(sql, ps);
            DataTable dt = ds.Tables[0];
            Model.MArticle m;
            if (dt.Rows.Count > 0)
            {
                m = new Model.MArticle(dt.Rows[0]);
            }
            else
            {
                m = null;
            }
            return m;
        }

        public List<Model.MArticle> GetArticleList(string keyStr, bool isShow, int pageSize, int pageIndex, out int count)
        {
            string sql = "SELECT a.*,b.`name` as cname FROM `cms_article` a LEFT JOIN `cms_column` b ON a.rColumn=b.id where a.title like CONCAT('%',?keyStr,'%') and a.isShow=?isShow order by a.addTime desc LIMIT ?count,?pageSize ";
            string sql01 = "SELECT count(*) FROM `cms_article` where title like CONCAT('%',?keyStr,'%') and isShow=?isShow ";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?keyStr",MySqlDbType.VarChar),
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?count",MySqlDbType.Int32),
                new MySqlParameter("?pageSize",MySqlDbType.Int32)
            };
            ps[0].Value = keyStr;
            ps[1].Value = isShow;
            ps[2].Value = (pageIndex - 1) * pageSize;
            ps[3].Value = pageSize;
            DataSet ds = Core.MySqlHelper.Query(sql, ps);

            MySqlParameter[] ps01 = new MySqlParameter[] { 
                new MySqlParameter("?keyStr",MySqlDbType.VarChar),
                new MySqlParameter("?isShow",MySqlDbType.Bit)
            };
            ps01[0].Value = keyStr;
            ps01[1].Value = isShow;
            DataSet ds01 = Core.MySqlHelper.Query(sql01, ps01);
            count = Convert.ToInt32(ds01.Tables[0].Rows[0][0]);

            DataTable dt = ds.Tables[0];
            List<Model.MArticle> list = new List<Model.MArticle>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MArticle m = new Model.MArticle(r);
                m.Column.Name = r["cname"].ToString();
                list.Add(m);
            }
            return list;
        }
        public List<Model.MArticle> GetArticleList(int columnId, string keyStr, bool isShow, int pageSize, int pageIndex, out int count)
        {
            if (columnId == 0)
            {
                return GetArticleList(keyStr, isShow, pageSize, pageIndex, out count);
            }
            else
            {
                string sql = "SELECT a.*,b.`name` as cname FROM `cms_article` a LEFT JOIN `cms_column` b ON a.rColumn=b.id where a.rColumn=?columnId and a.title like CONCAT('%',?keyStr,'%') LIMIT ?pageIndex,?pageSize";
                string sql01 = "SELECT count(*) FROM `cms_article` where rColumn=?columnId ";
                MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?columnId",MySqlDbType.Int32),
                new MySqlParameter("?keyStr",MySqlDbType.VarChar),
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?pageIndex",MySqlDbType.Int32),
                new MySqlParameter("?pageSize",MySqlDbType.Int32)
            };
                ps[0].Value = columnId;
                ps[1].Value = keyStr;
                ps[2].Value = isShow;
                ps[3].Value = (pageIndex - 1) * pageSize;
                ps[4].Value = pageSize;
                DataSet ds = Core.MySqlHelper.Query(sql, ps);

                MySqlParameter[] ps01 = new MySqlParameter[] { 
                new MySqlParameter("?columnId",MySqlDbType.Int32),
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?pageIndex",MySqlDbType.Int32),
                new MySqlParameter("?pageSize",MySqlDbType.Int32)
            };
                ps01[0].Value = columnId;
                ps01[1].Value = isShow;
                ps01[2].Value = (pageIndex - 1) * pageSize;
                ps01[3].Value = pageSize;
                DataSet ds01 = Core.MySqlHelper.Query(sql01, ps01);
                count = Convert.ToInt32(ds01.Tables[0].Rows[0][0]);
                DataTable dt = ds.Tables[0];
                List<Model.MArticle> list = new List<Model.MArticle>();
                foreach (DataRow r in dt.Rows)
                {
                    Model.MArticle m = new Model.MArticle(r);
                    m.Column.Name = r["cname"].ToString();
                    list.Add(m);
                }
                return list;
            }
        }
        
        public List<Model.MArticle> GetArticleList(bool isRecommend, bool isTop, bool isShow, int pageSize, int pageIndex, out int count)
        {
            string sql = "SELECT a.*,b.`name` as cname FROM `cms_article` a LEFT JOIN `cms_column` b ON a.rColumn=b.id where a.isRecommend=?isRecommend and a.isTop=?isTop and a.isShow=?isShow LIMIT ?count,?pageSize";
            string sql01 = "SELECT count(*) FROM `cms_article` where isRecommend=?isRecommend and isTop=?isTop and isShow=?isShow ";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?count",MySqlDbType.Int32),
                new MySqlParameter("?pageSize",MySqlDbType.Int32),
                new MySqlParameter("?isRecommend",MySqlDbType.Bit),
                new MySqlParameter("?isTop",MySqlDbType.Bit)
            };
            ps[0].Value = isShow;
            ps[1].Value = (pageIndex - 1) * pageSize;
            ps[2].Value = pageSize;
            ps[3].Value = isRecommend;
            ps[4].Value = isTop;
            DataSet ds = Core.MySqlHelper.Query(sql, ps);

            MySqlParameter[] ps01 = new MySqlParameter[] { 
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?isRecommend",MySqlDbType.Bit),
                new MySqlParameter("?isTop",MySqlDbType.Bit)
            };
            ps01[0].Value = isShow;
            ps01[1].Value = isRecommend;
            ps01[2].Value = isTop;
            DataSet ds01 = Core.MySqlHelper.Query(sql01, ps01);

            count = Convert.ToInt32(ds01.Tables[0].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            List<Model.MArticle> list = new List<Model.MArticle>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MArticle m = new Model.MArticle(r);
                m.Column.Name = r["cname"].ToString();
                list.Add(m);
            }
            return list;
        }
        public List<Model.MArticle> GetArticleList(DateTime start, DateTime end, bool isRecommend, bool isTop, bool isShow, int pageSize, int pageIndex, out int count)
        {
            string sql = "SELECT a.title,b.`name` as cname FROM `cms_article` a LEFT JOIN `cms_column` b ON a.rColumn=b.id where a.addTime>?start and a.addTime<?end LIMIT ?count,?pageSize";
            string sql01 = "SELECT count(*) FROM `cms_article` where title like CONCAT('%',?keyStr,'%') ";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?start",MySqlDbType.DateTime),
                new MySqlParameter("?end",MySqlDbType.DateTime),
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?count",MySqlDbType.Int32),
                new MySqlParameter("?pageSize",MySqlDbType.Int32)
            };
            ps[0].Value = start;
            ps[1].Value = end;
            ps[2].Value = isShow;
            ps[3].Value = (pageIndex - 1) * pageSize;
            ps[4].Value = pageSize;
            DataSet ds = Core.MySqlHelper.Query(sql, ps);
            DataSet ds01 = Core.MySqlHelper.Query(sql01);
            count = Convert.ToInt32(ds01.Tables[0].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            List<Model.MArticle> list = new List<Model.MArticle>();
            foreach (DataRow r in dt.Rows)
            {
                Model.MArticle m = new Model.MArticle(r);
                m.Column.Name = r["cname"].ToString();
                list.Add(m);
            }
            return list;
        }

        public bool AddArticle(Model.MArticle m)
        {
            string sql01 = "insert into `cms_article`(title,content,keyStr,source,rColumn,des,author,addTime,editTime,isRecommend,isShow,isTop)" +
                " values(?title,?content,?keyStr,?source,?rColumn,?des,?author,?addTime,?editTime,?isRecommend,?isShow,?isTop)";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?title",MySqlDbType.VarChar),
                new MySqlParameter("?content",MySqlDbType.LongText),
                new MySqlParameter("?keyStr",MySqlDbType.VarChar),
                new MySqlParameter("?source",MySqlDbType.VarChar),
                new MySqlParameter("?rColumn",MySqlDbType.Int32),
                new MySqlParameter("?des",MySqlDbType.VarChar),
                new MySqlParameter("?author",MySqlDbType.VarChar),
                new MySqlParameter("?addTime",MySqlDbType.DateTime),
                new MySqlParameter("?editTime",MySqlDbType.DateTime),
                new MySqlParameter("?isRecommend",MySqlDbType.Bit),
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?isTop",MySqlDbType.Bit)
            };
            ps[0].Value = m.Title;
            ps[1].Value = m.Content;
            ps[2].Value = m.Key;
            ps[3].Value = m.Source;
            ps[4].Value = m.RColumn;
            ps[5].Value = m.Des;
            ps[6].Value = m.Author;
            ps[7].Value = DateTime.Now.ToString();
            ps[8].Value = DateTime.Now.ToString();
            ps[9].Value = m.IsRecommend;
            ps[10].Value = m.IsShow;
            ps[11].Value = m.IsTop;
            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdArticle(Model.MArticle m)
        {
            string sql01 = "update `cms_article` set title=?title,content=?content,keyStr=?keyStr" +
                ",source=?source,rColumn=?rColumn,des=?des,editTime=?editTime,author=?author," +
                "isRecommend=?isRecommend,isShow=?isShow,isTop=?isTop where id=?id";
            MySqlParameter[] ps = new MySqlParameter[] { 
                new MySqlParameter("?id",MySqlDbType.Int32),
                new MySqlParameter("?title",MySqlDbType.VarChar),
                new MySqlParameter("?content",MySqlDbType.LongText),
                new MySqlParameter("?keyStr",MySqlDbType.VarChar),
                new MySqlParameter("?source",MySqlDbType.VarChar),
                new MySqlParameter("?rColumn",MySqlDbType.Int32),
                new MySqlParameter("?des",MySqlDbType.VarChar),
                new MySqlParameter("?author",MySqlDbType.VarChar),
                new MySqlParameter("?editTime",MySqlDbType.VarChar),
                new MySqlParameter("?isRecommend",MySqlDbType.Bit),
                new MySqlParameter("?isShow",MySqlDbType.Bit),
                new MySqlParameter("?isTop",MySqlDbType.Bit)
            };
            ps[0].Value = m.Id;
            ps[1].Value = m.Title;
            ps[2].Value = m.Content;
            ps[3].Value = m.Key;
            ps[4].Value = m.Source;
            ps[5].Value = m.RColumn;
            ps[6].Value = m.Des;
            ps[7].Value = m.Author;
            ps[8].Value = DateTime.Now.ToString();
            ps[9].Value = m.IsRecommend;
            ps[10].Value = m.IsShow;
            ps[11].Value = m.IsTop;

            if (Core.MySqlHelper.ExecuteSql(sql01, ps) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DelArticle(string ids)
        {
            string sql01 = "delete from `cms_article` where id in(" + ids + ")";
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
