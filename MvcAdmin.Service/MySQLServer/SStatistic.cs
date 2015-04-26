using MvcAdmin.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Service.MySQLServer
{
    public class SStatistic : IStatistic
    {
        public int GetAccountCount()
        {
            string sql01 = "SELECT count(*) FROM `sys_account`";

            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());

        }

        public int GetArticleCount()
        {
            string sql01 = "SELECT count(*) FROM `cms_article`";

            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int GetColumnCount()
        {
            string sql01 = "SELECT count(*) FROM `cms_column`";

            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int GetLinkCount()
        {
            string sql01 = "SELECT count(*) FROM `cms_link`";

            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int GetBaseContenrCount()
        {
            string sql01 = "SELECT count(*) FROM `cms_basecontent`";

            DataSet ds = Core.MySqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }
    }
}
