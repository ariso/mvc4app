using MvcAdmin.DAO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MvcAdmin.Service.MSSQLServer
{
    public class SStatistic : IStatistic
    {
        
        public int GetAccountCount()
        {
            string sql01 = "SELECT count(*) FROM [sys_account]";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int GetArticleCount()
        {
            string sql01 = "SELECT count(*) FROM [cms_article]";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int GetColumnCount()
        {
            string sql01 = "SELECT count(*) FROM [cms_column]";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int GetLinkCount()
        {
            string sql01 = "SELECT count(*) FROM [cms_link]";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int GetBaseContenrCount()
        {
            string sql01 = "SELECT count(*) FROM [cms_basecontent]";

            DataSet ds = Core.MSSqlHelper.Query(sql01);
            DataTable dt = ds.Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }
    }
}
