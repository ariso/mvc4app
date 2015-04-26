using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;


namespace MvcAdmin.Model
{
    public class MAccount
    {
        public MAccount() { }
        public MAccount(DataRow dr) 
        {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["accountName"] != null) { this.accountName = (string)dr["accountName"]; }
            if (dr["accountPwd"] != null) { this.accountPwd = (string)dr["accountPwd"]; }
            if (dr["rAccountGroup"] != null) { 
                this.rAccountGroup = (int)dr["rAccountGroup"];
                this.AccountGrounp = new MAccountGroup() { Id = this.rAccountGroup };
            }
            
        }
        
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string accountName;
        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }
        private string accountPwd;
        /// <summary>
        /// 账户密码
        /// </summary>
        public string AccountPwd
        {
            get { return accountPwd; }
            set { accountPwd = value; }
        }

        private int rAccountGroup;

        public int RAccountGroup
        {
            get { return rAccountGroup; }
            set { rAccountGroup = value; }
        }
        private MAccountGroup accountGrounp;

        public MAccountGroup AccountGrounp
        {
            get { return accountGrounp; }
            set { accountGrounp = value; }
        }
    }
}
