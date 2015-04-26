using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MModuleCategory
    {
        public MModuleCategory() { }
        public MModuleCategory(DataRow dr) {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["des"] != null) { this.des = (string)dr["des"]; }
            if (dr["categoryName"] != null) { this.categoryName = (string)dr["categoryName"]; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        private string des;

        public string Des
        {
            get { return des; }
            set { des = value; }
        }
    }
}
