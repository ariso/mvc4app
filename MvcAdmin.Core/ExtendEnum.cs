/* ----------------------------------------------------------
 * @名称     :    对枚举类型的扩展
 * @描述     :    获得枚举类型的值；
 *               （原有工具类基础上）
 * @创建人   :    
 * @创建日期 :    
 * @修改记录 : 
 * ----------------------------------------------------------*/

using System;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MvcAdmin.Core
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class ExtendEnum
    {
        /// <summary>
        /// 获得指定举值的字符串形式
        /// </summary>
        /// <param name="p_enum">枚举对象</param>
        /// <returns>返回枚举值的字符串形式</returns>
        /// <example>
        ///     <code>
        ///     //strTemp = "5";
        ///     string strTemp = DbType.Date.GetEnumValue();
        ///     </code>
        /// </example>
        public static string GetEnumValue(this Enum p_enum)
        {
            Type t = p_enum.GetType();

            FieldInfo fieldInfo = t.GetField("value__");

            object fieldValue = fieldInfo.GetValue(p_enum);

            string returnValue = fieldValue.ConvertToString();

            fieldValue = null;

            fieldInfo = null;

            return returnValue;
        }
    }
}
