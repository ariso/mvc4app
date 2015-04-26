using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MSite
    {
        public MSite(){
        
        }
        public MSite(DataRow dr){
            if (dr["siteId"] != null) { this.siteId = (string)dr["siteId"]; }
            if (dr["siteName"] != null) { this.siteName = (string)dr["siteName"]; }
            if (dr["siteDescription"] != null) { this.siteDescription = (string)dr["siteDescription"]; }
            if (dr["siteCopyright"] != null) { this.siteCopyright = (string)dr["siteCopyright"]; }
            if (dr["siteKeywords"] != null){this.siteKeywords = (string)dr["siteKeywords"];}
        }
        private string siteId;
        /// <summary>
        /// 
        /// </summary>
        public string SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }
        private string siteName;
        /// <summary>
        /// 
        /// </summary>
        public string SiteName
        {
            get { return siteName; }
            set { siteName = value; }
        }
        private string siteDescription;
        /// <summary>
        /// 
        /// </summary>
        public string SiteDescription
        {
            get { return siteDescription; }
            set { siteDescription = value; }
        }
        private string siteKeywords;
        /// <summary>
        /// 
        /// </summary>
        public string SiteKeywords
        {
            get { return siteKeywords; }
            set { siteKeywords = value; }
        }
        private string siteCopyright;
        /// <summary>
        /// 
        /// </summary>
        public string SiteCopyright
        {
            get { return siteCopyright; }
            set { siteCopyright = value; }
        }
    }
}
