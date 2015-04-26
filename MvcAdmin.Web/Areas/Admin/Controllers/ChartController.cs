using System.Web.Mvc;
using MvcAdmin.Web.MvcHelper;
using MvcAdmin.DAO;
using System.Web.SessionState;

namespace MvcAdmin.Web.Areas.Admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [AdminFilter]
    [ErrorFilter]
    public class ChartController : Controller
    {
        //
        // GET: /Admin/Chart/
        readonly IStatistic statistic;
        public ChartController(IStatistic statistic) { 
            this.statistic=statistic;
        }
        [OutputCache(Duration = 60,NoStore=true)]
        [LogFilter(actionContent="获取图表")]//暂未实现log记录
        public ActionResult Index()
        {

            ViewData["ArticleCount"] = statistic.GetArticleCount();
            ViewData["BaseContenrCount"] = statistic.GetBaseContenrCount();
            ViewData["ColumnCount"] = statistic.GetColumnCount();
            ViewData["LinkCount"] = statistic.GetLinkCount();
            return View("Index");
        }

    }
}
