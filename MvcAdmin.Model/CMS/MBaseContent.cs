using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MBaseContent
    {
        public MBaseContent() { }
        public MBaseContent(DataRow dr) {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["name"] != null) { this.name = Convert.ToString(dr["name"]); }
            if (dr["des"] != null) { this.des = Convert.ToString(dr["des"]); }
            if (dr["content"] != null) { this.content = Convert.ToString(dr["content"]); }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string des;

        public string Des
        {
            get { return des; }
            set { des = value; }
        }
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
