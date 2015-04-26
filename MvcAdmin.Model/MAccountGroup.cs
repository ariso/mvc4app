using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MAccountGroup
    {
        public MAccountGroup() { }
        public MAccountGroup(DataRow dr) 
        {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["groupName"] != null) { this.groupName = (string)dr["groupName"]; }
            if (dr["des"] != null) { this.des = (string)dr["des"]; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string groupName;

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }
        private string des;

        public string Des
        {
            get { return des; }
            set { des = value; }
        }
    }
}
