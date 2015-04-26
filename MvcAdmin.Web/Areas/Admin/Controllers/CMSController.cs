using MvcAdmin.DAO;
using MvcAdmin.Model;
using MvcAdmin.Web.MvcHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MvcAdmin.Web.Areas.Admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [AdminFilter]
    [ErrorFilter]
    public class CMSController : Controller
    {
        //
        // GET: /Admin/CMS/
        readonly IArticle article;
        readonly IColumn column;
        readonly ILink link;
        readonly IBaseContent baseContent;
        public CMSController(IArticle article, IColumn column, ILink link,IBaseContent baseContent)
        {
            this.article = article;
            this.column = column;
            this.link = link;
            this.baseContent = baseContent;
        }

        #region 文章
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ArticleList(int columnid, int page, string keystr)
        {
            int count=0;
            var rows = article.GetArticleList(columnid,keystr, true, 10, page, out count);
            return Json(new
            {
                success = true,
                rows = rows,
                count = count
            });
        }
        public ActionResult AddArticle()
        {
            ViewData["model"] = column.GetMcolumnList(true);
            ViewData["modelp"] = column.GetMcolumnList(true, 0);
            return View("ArticleAdd");
        }
        public ActionResult GetArticle(int id)
        {
            ViewData["model"] = column.GetMcolumnList(true);
            ViewData["modelp"] = column.GetMcolumnList(true, 0);

            ViewData["model01"] = article.GetArticle(id);
            return View("ArticleUpd");
        }
        [ValidateInput(false)]
        public ActionResult ArticleAdd(MArticle m)
        {
            if (article.AddArticle(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateInput(false)]
        public ActionResult ArticleUpd(MArticle m)
        {
            if (article.UpdArticle(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //
        public ActionResult DelArticle(string ids)
        {
            if (article.DelArticle(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 分类
        public ActionResult Column()
        {
            ViewData["model"] = column.GetMcolumnList(true);
            ViewData["modelp"] = column.GetMcolumnList(true,0);
            
            ViewData["model01"] = column.GetMcolumnList(false);
            return View("Column");
        }

        public ActionResult AddColumn()
        {
            ViewData["model"] = column.GetMcolumnList(true,0);
            return View("ColumnAdd");
        }

        public ActionResult GetColumn(int id)
        {
            ViewData["model"] = column.GetMcolumnList(true,0).FindAll(t=>t.Id!=id);
            ViewData["model01"] = column.GetMcolumn(id);
            return View("ColumnUpd");
        }

        public ActionResult ColumnAdd(MColumn m)
        {
            if (column.AddMcolumn(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ColumnUpd(MColumn m)
        {
            if (column.UpdMcolumn(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //
        public ActionResult ColumnDel(string ids)
        {
            //判断使用
            if (column.IsUserMcolumn(int.Parse(ids))) {
                return Json(new
                {
                    success = false,
                    msg = "该分类在文章中使用或者有下级子类"
                }, JsonRequestBehavior.AllowGet);
            }

            if (column.DelMcolumn(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region link
        public ActionResult LinkList() {
            ViewData["model"] = link.GetLinkList();
            return View("Link");
        }
        public ActionResult GetLink(int id)
        {
            ViewData["model"] = link.GetLink(id);
            return View("LinkUpd");
        }

        public ActionResult LinkAdd()
        {
            return View("LinkAdd");
        }
        public ActionResult AddLink(MLink m) {
            if (link.AddLink(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdLink(MLink m)
        {
            if (link.UpdLink(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //
        public ActionResult DelLink(string ids)
        {
            if (link.DelLink(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //

        #endregion

        #region baseContent

        public ActionResult baseContentList()
        {
            ViewData["model"] = baseContent.GetBaseContentList();
            return View("baseContent");
        }

        public ActionResult GetbaseContent(int id)
        {
            ViewData["model"] = baseContent.GetBaseContent(id);
            return View("BaseContentUpd");
        }

        public ActionResult BaseContentAdd()
        {
            return View("BaseContentAdd");
        }
        [ValidateInput(false)]
        public ActionResult AddbaseContent(MBaseContent m)
        {
            if (baseContent.AddBaseContent(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateInput(false)]
        public ActionResult UpdbaseContent(MBaseContent m)
        {
            if (baseContent.UpdBaseContent(m))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelbaseContent(string  ids)
        {
            if (baseContent.DelBaseContent(ids))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    msg = "系统错误"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
