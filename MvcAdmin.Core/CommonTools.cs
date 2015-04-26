/* ----------------------------------------------------------
 * @名称     :    工具类
 * @描述     :    集合了日常开发常用的方法
 * @创建人   :    
 * @创建日期 :    
 * @修改记录 :  
 * @修改1 :    
 * ----------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MvcAdmin.Core
{
    /// <summary>
    /// 通用工具类
    /// <para>
    /// 封装了开发过程中常用的方法。
    /// </para>    
    /// </summary>
    public class CommonTools
    {
        /// <summary>
        /// 终止指定名称的进程
        /// </summary>
        /// <param name="p_processName">进程的名称</param>        
        public static void KillProcessByName(string p_processName)
        {
            try
            {
                foreach (Process pro in Process.GetProcessesByName(p_processName))
                {
                    //如果当前的进程还没有退出，则终止此进程
                    if (!pro.HasExited)
                    {
                        pro.Kill();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 确定指定的目录是否存
        /// </summary>
        /// <remarks>
        /// 如果目录不存在，则会建立此目录。
        /// 如果建立失败将返回false。
        /// </remarks>
        /// <param name="p_filePath">目录名称</param>
        /// <returns>如果存在返回true,否则返回false。</returns>
        public static bool ExistsDirectory(string p_filePath)
        {
            bool returnValue = true;
            //生成的文件名称和路径            
            if (!Directory.Exists(p_filePath))
            {
                try
                {
                    Directory.CreateDirectory(p_filePath);
                }
                catch
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 随机生成的文件名称
        /// </summary>
        /// <remarks>
        /// 根据日期时间生成名称，例如:20090101111111。
        /// </remarks>
        /// <returns>生成的文件名称</returns>
        public static string RandomFileName()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }

        /// <summary>
        /// 随机生成指定扩展名的文件名称
        /// </summary>
        /// <remarks>
        /// 在指定扩展名时，必须加上“.”。
        /// </remarks>
        /// <param name="p_fileExtension">扩展名</param>
        /// <returns>生成的文件名称</returns>
        /// <example>
        ///     <code>
        ///     string strFileName = CommonTools.RandomFileName(".xml");
        ///     </code>
        /// </example>
        public static string RandomFileName(string p_fileExtension)
        {
            StringBuilder sb = new StringBuilder(DateTime.Now.ToString("yyyyMMddhhmmss"));
            sb.Append(p_fileExtension);
            return sb.ToString();
        }

        /// <summary>
        /// 连接字符串
        /// </summary>       
        /// <remarks>
        /// 使用指定连接符号，将传入的对象连接成字符串形式。
        /// </remarks>
        /// <param name="p_connectSymbol">连接符号</param>
        /// <param name="p_items">连接对象数组</param>
        /// <returns>连接后的新字符串</returns>
        /// <example>
        ///     <code>
        ///     //strArray 返回"1,2,3";
        ///     string strArray = CommonTools.ConnectString(",","1","2","3");        
        ///     </code>
        /// </example>
        public static string ConnectString(string p_connectSymbol, params object[] p_items)
        {
            StringBuilder sb = new StringBuilder();
            string returnValue = string.Empty;
            string strSymbol = p_connectSymbol.ConvertToString();

            foreach (object item in p_items)
            {
                sb.Append(item)
                  .Append(strSymbol);
            }

            if (sb.Length > 0)
            {
                sb.Length -= strSymbol.Length;
            }

            returnValue = sb.ToString();
            sb = null;

            return returnValue;
        }

        /// <summary>
        /// 动态生成对象
        /// </summary>
        /// <remarks>
        /// 根据指定的程序集名称和类型名称，动态生成对象。
        /// </remarks>
        /// <param name="p_assemblyName">程序集名称</param>
        /// <param name="p_objectName">此程集中的类名称,必须有命名空间</param>
        /// <returns>生成的对象</returns>
        /// <example>
        ///     <code>
        ///     object obj = CommonTools.DynamicObject("Pdwy.Core","Pdwy.Core.PageHelper");
        ///     </code>
        /// </example>
        public static object DynamicObject(string p_assemblyName, string p_objectName)
        {
            Assembly assembly = Assembly.Load(p_assemblyName);

            return assembly.CreateInstance(p_objectName, true);
        }
    }
}
