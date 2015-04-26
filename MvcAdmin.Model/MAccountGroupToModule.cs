using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MAccountGroupToModule
    {
        public MAccountGroupToModule() { }
        public MAccountGroupToModule(DataRow dr) 
        {
            //bool b=dr.Table.Columns.Contains("rModule");
            if (dr.Table.Columns.Contains("id") && dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr.Table.Columns.Contains("rModule") && dr["rModule"] != null)
            { 
                this.rModule = (int)dr["rModule"];
                MModule m = new MModule() { Id = this.rModule };
                if (dr.Table.Columns.Contains("moduleName")&&dr["moduleName"] != null) { m.ModuleName = dr["moduleName"].ToString(); }
                if (dr.Table.Columns.Contains("commandParameter") && dr["commandParameter"] != null) { m.CommandParameter = dr["commandParameter"].ToString(); }
                if (dr.Table.Columns.Contains("categoryId") && dr["categoryId"] != null) { m.CategoryId = int.Parse(dr["categoryId"].ToString()); }
                this.Module = m;
            }
            if (dr.Table.Columns.Contains("rAccountGroup") && dr["rAccountGroup"] != null)
            { 
                this.rAccountGroup = (int)dr["rAccountGroup"];
                MAccountGroup  m= new MAccountGroup() { Id = this.rAccountGroup };
                
                this.AccountGroup = m;
            }
            if (dr.Table.Columns.Contains("isEnable") && dr["isEnable"] != null) { this.IsEnable = Convert.ToBoolean(dr["isEnable"]); }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private MAccountGroup accountGroup;
        
        public MAccountGroup AccountGroup
        {
            get { return accountGroup; }
            set { accountGroup = value; }
        }

        private int rAccountGroup;

        public int RAccountGroup
        {
            get { return rAccountGroup; }
            set { rAccountGroup = value; }
        }
        
        private MModule module;
        
        public MModule Module
        {
            get { return module; }
            set { module = value; }
        }

        private int rModule;

        public int RModule
        {
            get { return rModule; }
            set { rModule = value; }
        }

        private bool isEnable;

        public bool IsEnable
        {
            get { return isEnable; }
            set { isEnable = value; }
        }

        
    }
}
