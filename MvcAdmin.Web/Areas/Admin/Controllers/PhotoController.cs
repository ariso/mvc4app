using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdmin.Core;
using MvcAdmin.Web.MvcHelper;
using System.IO;
using System.Web.SessionState;

namespace MvcAdmin.Web.Areas.Admin.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    [AdminFilter]
    [ErrorFilter]
    public class PhotoController : Controller
    {
        //
        // GET: /Admin/Photo/

        public ActionResult Index(string dirStr)
        {
            string dir = "/Photo";
            if (!string.IsNullOrEmpty(dirStr)) {
                dir = dirStr;
            }
            ViewData["model"] = dir;
            return View();
        }

        public ActionResult GetDir(string dirStr)
        {
            string path = "~" + dirStr + "/";
            string indir = dirStr;
            string nextdir = dirStr + "/../";/*对应上级目录*/
            string apppath = Server.MapPath("/");
            string temppath = Server.MapPath(nextdir);/*对应上级目录绝对路径*/
            temppath = temppath.Replace(@"\", "/");
            apppath = apppath.Replace(@"\", "/");
            temppath = temppath.Substring(0, temppath.Length - 1);
            apppath = apppath.Substring(0, apppath.Length - 1);
            nextdir = temppath.Substring(apppath.Length, temppath.Length - apppath.Length);

            //int r = temppath.LastIndexOf("/");
            //nextdir = temppath.Substring(r , temppath.Length - r);


            dirStr = dirStr + "/";
            string[] files = PublicClass.FilePathToPath(path, dirStr);
            string[] filestime = PublicClass.FilePathToPathTime(files);
            return Json(new
            {
                success = true,
                nextdir = nextdir,
                files = files,
                indir = indir,
                filestime = filestime,
                dirs = PublicClass.DirPathToPath(path, dirStr),
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateDir(string dirStr)
        {
            ViewData["model"] = dirStr;
            return View("CreateDir");
        }

        public ActionResult DirCreate(string dirStr, string dirName)
        {

            if (DirFileHelper.CreateDirectory(Server.MapPath(dirStr + "/" + dirName)))
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
                    msg = "创建文件夹错误 dir error"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FileUpload(string dirStr)
        {
            ViewData["model"] = dirStr;
            return View("Upload");
        }

        public ActionResult UploadFile(string dirStr, HttpPostedFileBase fileimg)
        {
            //
            string name = PublicClass.FileHandler(fileimg);
            if (!string.IsNullOrEmpty(name))
            {
                string path = Server.MapPath(dirStr + "/" + name);
                fileimg.SaveAs(path);
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
                    msg = "文件错误 file error"
                }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DelDir(string dirStr) {
            
            //DirFileHelper.DeleteDirectory(s);
            try
            {
                string[] strr = dirStr.Split('|');
                for (int i = 0; i<strr.Length; i++) {
                    string s = Server.MapPath(strr[i]);
                    DirFileHelper.DeleteDirectory(s);
                }

                
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    msg = "文件错误 file error"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DelFile(string fileStr)
        {
            try
            {
                string s = Server.MapPath(fileStr);
                DirFileHelper.DeleteFile(s);
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(new
                {
                    success = false,
                    msg = "文件错误 file error"
                }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
