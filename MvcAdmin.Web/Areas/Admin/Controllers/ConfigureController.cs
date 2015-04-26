using MvcAdmin.Web.MvcHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Xml;

namespace MvcAdmin.Web.Areas.Admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [AdminFilter]
    [ErrorFilter]
    public class ConfigureController : Controller
    {
        //
        // GET: /Admin/Configure/
        public ConfigureController() { }

        public ActionResult Index()
        {
            //XmlNodeList list = SiteConfig.ReadAllChild("/site");
            ViewData["model"] = SiteConfig.ReadAllChild("site/item");
            return View();
        }

    }
}
