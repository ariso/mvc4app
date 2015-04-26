using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MvcAdmin.Model
{
    public class MArticle
    {
        public MArticle() { }
        public MArticle(DataRow dr) {
            if (dr["id"] != null) { this.id = (int)dr["id"]; }
            if (dr["title"] != null) { this.title = (string)dr["title"]; }
            if (dr["content"] != null) { this.content = (string)dr["content"]; }
            if (dr["addTime"] != null) { this.addTime = Convert.ToString(dr["addTime"]); }
            if (dr["editTime"] != null) { this.editTime = Convert.ToString(dr["editTime"]); }
            if (dr["des"] != null) { this.des = (string)dr["des"].ToString(); }
            if (dr["rColumn"] != null) { 
                this.rColumn = (int)dr["rColumn"];
                this.column = new MColumn() { Id = this.rColumn };
            }
            if (dr["keyStr"] != null) { this.key = (string)dr["keyStr"].ToString(); }
            if (dr["source"] != null) { this.source = (string)dr["source"].ToString(); }
            if (dr["author"] != null) { this.author = (string)dr["author"].ToString(); }
            if (dr["isRecommend"] != null) { this.isRecommend = Convert.ToBoolean(dr["isRecommend"]); }
            if (dr["isShow"] != null) { this.isShow = Convert.ToBoolean(dr["isShow"]); }
            if (dr["isTop"] != null) { this.isTop = Convert.ToBoolean(dr["isTop"]); }
        
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string addTime;

        public string AddTime
        {
            get { return addTime; }
            set { addTime = value; }
        }
        private string editTime;

        public string EditTime
        {
            get { return editTime; }
            set { editTime = value; }
        }
        private bool isTop;

        public bool IsTop
        {
            get { return isTop; }
            set { isTop = value; }
        }
        private bool isShow;

        public bool IsShow
        {
            get { return isShow; }
            set { isShow = value; }
        }
        private bool isRecommend;

        public bool IsRecommend
        {
            get { return isRecommend; }
            set { isRecommend = value; }
        }
        private string des;

        public string Des
        {
            get { return des; }
            set { des = value; }
        }
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string source;

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        private int rColumn;

        public int RColumn
        {
            get { return rColumn; }
            set { rColumn = value; }
        }
        private MColumn column;

        public MColumn Column
        {
            get { return column; }
            set { column = value; }
        }
    }
}
