using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Xml;

namespace MvcAdmin.Web.MvcHelper
{
    #region XML资源管理类 原作者:Artech
    public abstract class FileResourceManager : ResourceManager
    {
        private string baseName;
        public string Directory { get; private set; }
        public string Extension { get; private set; }

        public override string BaseName
        {
            get { return baseName; }
        }

        public FileResourceManager(string directory, string baseName, string extension)
        {
            this.Directory = directory;
            this.baseName = baseName;
            this.Extension = extension;
        }

        protected override string GetResourceFileName(CultureInfo culture)
        {
            string fileName = string.Format("{0}.{1}.{2}", this.baseName, culture, this.Extension.TrimStart('.'));
            string path = Path.Combine(this.Directory, fileName);
            if (File.Exists(path))
            {
                return path;
            }
            return Path.Combine(this.Directory, string.Format("{0}.{1}", baseName, this.Extension.TrimStart('.')));
        }
    }
    public class XmlResourceManager : FileResourceManager
    {
        public XmlResourceManager(string directory, string baseName)
            : base(directory, baseName, ".xml")
        { }

        protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            return new XmlResourceSet(this.GetResourceFileName(culture));
        }
    }
    public class XmlResourceReader : IResourceReader
    {
        public XmlDocument Document { get; private set; }
        public XmlResourceReader(string fileName)
        {
            this.Document = new XmlDocument();
            this.Document.Load(fileName);
            //using (XmlWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            //{
            //    this.Document.WriteTo(writer);
            //}
        }
        public XmlResourceReader(Stream stream)
        {
            this.Document = new XmlDocument();
            this.Document.Load(stream);
        }
        public IDictionaryEnumerator GetEnumerator()
        {
            Dictionary<string, string> set = new Dictionary<string, string>();
            foreach (XmlNode item in this.Document.GetElementsByTagName("item"))
            {
                //set.Add(item.Attributes["name"].Value, item.Attributes["value"].Value);
                //set.Add(item.Attributes["name"].Value, item.InnerText);
                //set.Add(item.Attributes["name"].Value,
                //    HttpContext.Current.Server.HtmlDecode(item.InnerText));
                set.Add(item.Attributes["name"].Value,
                    HttpUtility.UrlDecode(item.InnerText));
               
            }
            return set.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Dispose() { }
        public void Close() { }
    }
    public class XmlResourceSet : ResourceSet
    {
        public XmlResourceSet(Stream stream)
        {
            this.Reader = new XmlResourceReader(stream);
            this.Table = new Hashtable();
            this.ReadResources();
        }
        public XmlResourceSet(string fileName)
        {
            base.Reader = new XmlResourceReader(fileName);
            base.Table = new Hashtable();
            this.ReadResources();
        }
        public override Type GetDefaultReader()
        {
            return typeof(XmlResourceReader);
        }
        public override Type GetDefaultWriter()
        {
            return typeof(XmlResourceWriter);
        }
    }
    public class XmlResourceWriter : IResourceWriter
    {
        public XmlDocument Document { get; private set; }
        private string fileName;
        private XmlElement root;

        public XmlResourceWriter(string fileName)
        {
            this.fileName = fileName;
            this.Document = new XmlDocument();
            this.Document.AppendChild(this.Document.CreateXmlDeclaration("1.0", "utf-8", null));
            this.root = this.Document.CreateElement("resources");
            this.Document.AppendChild(this.root);
        }

        public void AddResource(string name, byte[] value)
        {
            throw new NotImplementedException();
        }

        public void AddResource(string name, object value)
        {
            throw new NotImplementedException();
        }

        public void AddResource(string name, string value)
        {
            //var node = this.Document.CreateElement("item");
            //node.SetAttribute("name", name);
            //node.SetAttribute("value", value);
            //this.root.AppendChild(node);
            throw new NotImplementedException();
        }

        public void Generate()
        {
            //using (XmlWriter writer = new XmlTextWriter(this.fileName, Encoding.UTF8))
            //{
            //    this.Document.WriteTo(writer);
            //}
            this.Document.Save(this.fileName);
        }
        public void Dispose() { }
        public void Close() { }
    }
    public class XmlResourceProvider : IResourceProvider
    {
        public XmlResourceManager ResourceManager { get; private set; }

        public XmlResourceProvider(string directory, string baseName)
        {
            this.ResourceManager = new XmlResourceManager(directory, baseName);
        }

        public object GetObject(string resourceKey, CultureInfo culture)
        {
            return this.ResourceManager.GetObject(resourceKey, culture);
        }
        public IResourceReader ResourceReader
        {
            get
            {
                return new XmlResourceReader(Path.Combine(this.ResourceManager.Directory, this.ResourceManager.BaseName + ".xml"));
            }
        }

        public object GetObject(string key)
        {
            return this.ResourceManager.GetObject(key);
        }

        object IResourceProvider.GetObject(string resourceKey, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return this.ResourceManager.GetObject(resourceKey, culture);
        }
    }
    public class XmlResourceProviderFactory : ResourceProviderFactory
    {
        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            string directory = HttpContext.Current.Server.MapPath("App_GlobalResources");
            return new XmlResourceProvider(directory, classKey);
        }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            string directory = HttpContext.Current.Server.MapPath(VirtualPathUtility.GetDirectory(virtualPath));
            string baseName = VirtualPathUtility.GetFileName(virtualPath);
            return new XmlResourceProvider(directory, baseName);
        }
    }
    //<system.web>
    //<globalization uiCulture="zh-CN" resourceProviderFactoryType="MvcAdmin.MvcHelper.XmlResourceProviderFactory"/>
    #endregion


    /// <summary>
    /// 资源扩展
    /// </summary>
    public static class Resource
    {
        /// <summary>
        /// 获取全局资源对象
        /// </summary>
        /// <param name="html">Html辅助类</param>
        /// <param name="classKey">类键值</param>
        /// <param name="resourceKey">资源键</param>
        /// <returns>资源对象</returns>
        public static Object GetGlobalResourceObject(this HtmlHelper html, String classKey, String resourceKey)
        {
            return GetGlobalResourceObject(html, classKey, resourceKey,null);
        }

        /// <summary>
        /// 获取全局资源对象
        /// </summary>
        /// <param name="html">Html辅助类</param>
        /// <param name="classKey">类键值</param>
        /// <param name="resourceKey">资源键</param>
        /// <param name="culture">文化信息</param>
        /// <returns>资源对象</returns>
        public static Object GetGlobalResourceObject(this HtmlHelper html, String classKey, String resourceKey, CultureInfo culture)
        {
            if (culture == null)
                culture = CultureInfo.CurrentUICulture;

            return html.ViewContext.HttpContext.GetGlobalResourceObject(classKey, resourceKey, culture);
        }

        /// <summary>
        /// 获取全局资源字符串
        /// </summary>
        /// <param name="html">Html辅助类</param>
        /// <param name="classKey">类键值</param>
        /// <param name="resourceKey">资源键</param>
        /// <returns>资源对象</returns>
        public static String GetGlobalResourceAsString(this HtmlHelper html, String classKey, String resourceKey)
        {
            return GetGlobalResourceAsString(html, classKey, resourceKey, null);
        }

        /// <summary>
        /// 获取全局资源字符串
        /// </summary>
        /// <param name="html">Html辅助类</param>
        /// <param name="classKey">类键值</param>
        /// <param name="resourceKey">资源键</param>
        /// <param name="culture">文化信息</param>
        /// <returns>资源对象</returns>
        public static String GetGlobalResourceAsString(this HtmlHelper html, String classKey, String resourceKey, CultureInfo culture)
        {
            return GetGlobalResourceObject(html, classKey, resourceKey, culture) as String;
        }
    }
}