using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MColumn
    {
        public MColumn(){ }
        public MColumn(DataRow dr) {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["name"] != null) { this.name = (string)dr["name"]; }
            if (dr["isNode"] != null) { this.isNode = Convert.ToBoolean(dr["isNode"]); }
            if (dr["link"] != null) { this.link = (string)dr["link"].ToString(); }
            if (dr["parentId"] != null) {
                this.parentId = string.IsNullOrEmpty(dr["parentId"].ToString()) ? 0 : (int)dr["parentId"];
                this.parent = new MColumn() { Id = this.parentId };
            }
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
        private bool isNode;

        public bool IsNode
        {
            get { return isNode; }
            set { isNode = value; }
        }
        private int parentId;

        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }
        private MColumn parent;

        public MColumn Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        private string link;

        public string Link
        {
            get { return link; }
            set { link = value; }
        }
    }
}
