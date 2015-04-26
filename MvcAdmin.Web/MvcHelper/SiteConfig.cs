using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MvcAdmin.Core;
using System.Xml;
using System.IO;

namespace MvcAdmin.Web.MvcHelper
{
    public class SiteConfig
    {
        private static string _path = ConfigurationManager.AppSettings["site"].ToString();
        public static string GetValue(string key) {
            string path = _path;
            XMLProcess xml = new XMLProcess(path);
            return xml.Read("/site/item[@name='" + key + "']");
        }
        public static string GetValue(string key,string attr)
        {
            string path = _path;
            XMLProcess xml = new XMLProcess(path);
            return xml.Read("/site/item[@name='" + key + "']", attr);
        }
        public static bool UpdValue(string key, string value)
        {
            string path = _path;
            XMLProcess xml = new XMLProcess(path);
            try
            {
                xml.Update("/site/item[@name='" + key + "']", value);
                return true;
            }
            catch {
                return false;
            }
            
        }
        public static bool UpdValue(string key,string attr, string value)
        {
            string path = _path;
            XMLProcess xml = new XMLProcess(path);
            try
            {
                xml.Update("/site/item[@name='" + key + "']", attr, value);
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// XPath表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static XmlNodeList ReadAllChild(string XPath)
        {
            XmlDocument doc = XMLLoad();
            //XmlNode xn = doc.SelectSingleNode(node);
            XmlNodeList nodelist = doc.SelectNodes(XPath);  //得到该节点的子节点
            return nodelist;
        }
        private static XmlDocument XMLLoad()
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + _path;
                if (File.Exists(filename)) xmldoc.Load(filename);
            }
            catch (Exception e)
            { }
            return xmldoc;
        }
    }
}