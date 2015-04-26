using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MLink
    {
        public MLink() { }
        public MLink(DataRow dr) {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["linkName"] != null) { this.linkName = Convert.ToString(dr["linkName"]); }
            if (dr["linkUrl"] != null) { this.linkUrl = Convert.ToString(dr["linkUrl"]); }
            if (dr["linkImg"] != null) { this.linkImg = Convert.ToString(dr["linkImg"]); }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string linkName;

        public string LinkName
        {
            get { return linkName; }
            set { linkName = value; }
        }
        private string linkUrl;

        public string LinkUrl
        {
            get { return linkUrl; }
            set { linkUrl = value; }
        }
        private string linkImg;

        public string LinkImg
        {
            get { return linkImg; }
            set { linkImg = value; }
        }
    }
}
