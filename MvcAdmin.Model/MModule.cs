using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MModule
    {
        public MModule() { }
        public MModule(DataRow dr) 
        {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["moduleName"] != null) { this.moduleName = (string)dr["moduleName"]; }
            if (dr["categoryId"] != null) {
                this.categoryId = (int)dr["categoryId"];
                this.moduleCategory = new MModuleCategory() { Id = this.categoryId };
            }
            if (dr["commandParameter"] != null) { this.commandParameter = (string)dr["commandParameter"]; }

        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string moduleName;

        public string ModuleName
        {
            get { return moduleName; }
            set { moduleName = value; }
        }
        private string commandParameter;

        public string CommandParameter
        {
            get { return commandParameter; }
            set { commandParameter = value; }
        }
        private int categoryId;

        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        private MModuleCategory moduleCategory;

        public MModuleCategory ModuleCategory {
            get { return moduleCategory; }
            set { moduleCategory = value; }
        }
    }
}
