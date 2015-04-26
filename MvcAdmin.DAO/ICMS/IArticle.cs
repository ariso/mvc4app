using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcAdmin.Model;

namespace MvcAdmin.DAO
{
    public interface IArticle
    {
        Model.MArticle GetArticle(int id);

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyStr">标题和关键字和描述？</param>
        /// <param name="isShow"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<Model.MArticle> GetArticleList(int columnId, string keyStr, bool isShow, int pageSize, int pageIndex, out int count);

        List<Model.MArticle> GetArticleList(bool isRecommend, bool isTop, bool isShow, int pageSize, int pageIndex, out int count);
        
        List<Model.MArticle> GetArticleList(DateTime start, DateTime end, bool isRecommend, bool isTop, bool isShow, int pageSize, int pageIndex, out int count);
        
        bool AddArticle(MArticle m);
        bool UpdArticle(MArticle m);
        bool DelArticle(string ids);
    }
}
