/* ----------------------------------------------------------
 * @名称     :    对集合类型的扩展
 * @描述     :    判断集合不为null或不为空 
 * @创建人   :    
 * @创建日期 :    
 * @修改记录 : 
 *    @修改1 :    添加对DataSet,DataTable,DataRow为null或为空的验证方法
 *    @修改2 :    添加注释
 * ----------------------------------------------------------*/

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace MvcAdmin.Core
{
    /// <summary>
    /// 集合类型扩展类
    /// </summary>
    public static class ExtendCollection
    {
        /// <summary>
        /// 确定对象是否为null或为空
        /// </summary>              
        /// <remarks>针对实现IList接口的对象</remarks>
        /// <param name="p_list">实现IList接口的类型</param>
        /// <returns>如果对象为null或集合中无元素，则返回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this IList p_list)
        {
            return p_list.NotNull() && p_list.Count > 0;
        }

        /// <summary>
        /// 确定对象是否为null或为空
        /// </summary>
        /// <remarks>针对实现ICollection接口的类型</remarks>
        /// <param name="p_collection">实现ICollection接口的对象</param>
        /// <returns>如果对象为null或集合中无元素，则返回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this ICollection p_collection)
        {

            return p_collection.NotNull() && p_collection.Count > 0;
        }

        /// <summary>
        /// 确定对象是否为null或为空
        /// </summary>
        /// <remarks>针对实现IDictionary接口的类型</remarks>
        /// <param name="p_dictionary">实现IDictionary接口的对象</param>
        /// <returns>如果对象为null或集合中无元素，则返回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this IDictionary p_dictionary)
        {
            return p_dictionary.NotNull() && p_dictionary.Count > 0;
        }

        /// <summary>
        /// 确定对象是否为null或为空
        /// </summary>
        /// <remarks>针对实现IEnumerable接口的类型</remarks>
        /// <param name="p_enumerable">实现IEnumerable接口的对象</param>
        /// <returns>如果对象为null或集合中无元素，则返回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this IEnumerable p_enumerable)
        {
            return p_enumerable.NotNull() && p_enumerable.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// 确定对象是否为null或为空
        /// </summary>
        /// <remarks>针对DataTable类型</remarks>
        /// <param name="p_dataTable">DataTable类型的对象</param>
        /// <returns>如果对象为null或集合中无元素，则返回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this DataTable p_dataTable)
        {
            return p_dataTable.NotNull() && p_dataTable.Rows.Count > 0;
        }

        /// <summary>
        /// 确定对象是否为null或为空
        /// </summary>
        /// <remarks>针对DataSet类型</remarks>
        /// <param name="p_dataSet">DataSet类型的对象</param>
        /// <returns>如果对象为null或集合中无元素，则返回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this DataSet p_dataSet)
        {
            return p_dataSet.NotNull() && p_dataSet.Tables.Count > 0;
        }

        /// <summary>
        /// 确定对象是否为null或为空
        /// </summary>
        /// <remarks>针对DataRow类型</remarks>
        /// <param name="p_dataRow">DataRow类型的对象</param>
        /// <returns>如果对象为null或集合中无元素，则返回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this DataRow p_dataRow)
        {
            return p_dataRow.NotNull() && p_dataRow.ItemArray.Length > 0;
        }

        /// <summary>
        /// 将集合中的元素连接成字符串
        /// </summary>
        /// <remarks>
        /// 使用指定的连接符，将一个集合中的所有元素连接起来，形式一个字符串。
        /// </remarks>
        /// <param name="p_array">实现ICollection接口的对象</param>
        /// <param name="p_connectString">连接符</param>
        /// <returns>返回连接后的字符串</returns>
        /// <example>
        ///     <code>
        ///     int []intArray = {1,2,3,4,5,6};
        ///     //strTemp 将返回"1,2,3,4,5,6"
        ///     string strTemp = intArray.Concat(",");
        ///     </code>
        ///     <code>
        ///     int []intArray = {1,2,3,4,5,6};
        ///     //strTemp 将返回"123456"
        ///     string strTemp = intArray.Concat(null);
        ///     </code>
        /// </example>
        public static string Concat(this ICollection p_array, string p_connectString)
        {
            StringBuilder sb = new StringBuilder();
            string strConnectString = p_connectString.ConvertToString();
            foreach (var item in p_array)
            {
                sb.Append(item);
                sb.Append(strConnectString);
            }
            if (sb.Length > 0)
            {
                sb.Length -= strConnectString.Length;
            }
            string returnValue = sb.ToString();
            sb = null;
            return returnValue;
        }
    }
}
