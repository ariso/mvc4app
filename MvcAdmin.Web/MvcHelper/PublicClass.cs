using MvcAdmin.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MvcAdmin.Web.MvcHelper
{
    public class PublicClass
    {

        /// <summary>
        /// 判断是否图片类型
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FileHandler(HttpPostedFileBase file)
        {
            try
            {
                string FileType = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);//取得类型
                FileType = FileType.ToLower();
                if (FileType == "gif" || FileType == "jpg" || FileType == "jpeg" || FileType == "png" )
                {
                    //新的文件名
                    string ImgName =  DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + FileType;
                    return ImgName;
                }
                else
                {
                    return "";
                }
            }
            catch { return ""; }
        }
        /// <summary>
        /// 返回文件时间的数组
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string[] FilePathToPathTime(string[] paths)
        {
            //string temppath = HttpContext.Current.Server.MapPath(path);
            //string[] s = DirFileHelper.GetFileNames(temppath);
            string[] s = new string[paths.Length];
            for(int i=0;i<paths.Length;i++)
            {
                string temppath = HttpContext.Current.Server.MapPath(paths[i]);
                FileInfo file = new FileInfo(temppath);
                s[i] = file.CreationTime.ToString();
            }
            return s;
        }
        /// <summary>
        /// 带盘符的绝对文件地址转换为相对地址
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] FilePathToPath(string path)
        {
            string temppath = HttpContext.Current.Server.MapPath(path);
            string[] s = DirFileHelper.GetFileNames(temppath);

            return PathToPath(s, temppath, path);
        }
        public static string[] FilePathToPath(string path,string xpath)
        {
            string temppath = HttpContext.Current.Server.MapPath(path);
            string[] s = DirFileHelper.GetFileNames(temppath);

            return PathToPath(s, temppath, xpath);
        }
        /// <summary>
        /// 带盘符的绝对目录地址转换为不带盘符的绝对地址
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] DirPathToPath(string path)
        {
            string temppath = HttpContext.Current.Server.MapPath(path);
            string[] s = DirFileHelper.GetDirectories(temppath);
            
            return PathToPath(s, temppath, path);
        }
        public static string[] DirPathToPath(string path, string xpath)
        {
            string temppath = HttpContext.Current.Server.MapPath(path);
            string[] s = DirFileHelper.GetDirectories(temppath);

            return PathToPath(s, temppath, xpath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="temppath"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string[] PathToPath(string[] s,string temppath,string path)
        {
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = s[i].Replace(@"\", "/");
                int r = s[i].LastIndexOf("/");
                s[i] = s[i].Substring(r + 1, s[i].Length - r - 1);
                s[i] = path + s[i];
            }
            return s;
        }
    }
}