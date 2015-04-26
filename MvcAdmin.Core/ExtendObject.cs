/* ----------------------------------------------------------
 * @名称     :    对象的扩展
 * @描述     :    判断对象不为空；将对象（可为null)转换成字符串;反射对象属性集合，赋值和获值.
 * @创建人   :    
 * @创建日期 :    
 * @修改记录 : 
 *    @修改1 :     添加ParseExpression方法，可以实现针对某个对象和对象表达式进行解析
 *    @修改2 :    
 * ----------------------------------------------------------*/

using System;
using System.Reflection;

namespace MvcAdmin.Core
{
    /// <summary>
    /// 日期数据的格式枚举
    /// </summary>
    public enum DateFormat
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 长日期
        /// </summary>
        ShortDate,
        /// <summary>
        /// 短日期
        /// </summary>
        //ShortTime,
        ///// <summary>
        ///// 短时间
        ///// </summary>
        LongDate,
        ///// <summary>
        ///// 长时间
        ///// </summary>
        //LongTime
    }
    /// <summary>
    /// 对象扩展类
    /// </summary>
    public static class ExtendObject
    {
        /// <summary>
        /// 确定对象是否为null
        /// </summary>
        /// <param name="p_object">要确定的对象</param>        
        /// <returns>如果对象为null，则返回false，否则返回true。</returns>        
        public static bool NotNull(this object p_object)
        {
            return p_object != null;
        }

        /// <summary>
        /// 将对象转换成字符串
        /// </summary>
        /// <remarks>
        /// 当对象为null,返回空字符串，否则返回对象的ToString()。
        /// </remarks>
        /// <param name="p_object">要转换的对象</param>
        /// <returns>返回的字符串</returns>
        /// <example>
        ///     <code>
        ///     object obj = null;
        ///     //strTemp1等于空
        ///     string strTemp1 = obj.ConvertToString();        
        ///     obj = 123456;
        ///     //strTemp2等于"123456"
        ///     string strTemp2 = obj.ConvertToString();        
        ///     </code>
        /// </example>
        public static string ConvertToString(this object p_object)
        {
            string returnValue = string.Empty;
            if (p_object.NotNull())
            {
                returnValue = p_object.ToString();
            }
            return returnValue;
        }

        /// <summary>
        /// 将对象转成指定类型,且类型不能为null。
        /// </summary>
        /// <remarks>
        /// 转换类型时，转换双方都必须是基础类型,即string、int、float、bool、double、char等。
        /// </remarks>
        /// <param name="p_object">将要转换的对象</param>
        /// <param name="p_type">所要转换的类型</param>
        /// <returns>转换后的对象</returns>
        /// <example>
        ///     <code>
        ///     string str = "1";
        ///     int i = 0;
        ///     i = (int)str.ConvertToValueType(typeof(int));
        ///     int? j = null;        
        ///     j = str.ConvertToValueType(typeof(int?)) as int?;
        ///     </code>
        /// </example>
        public static object ConvertToValueType(this object p_object, Type p_type)
        {
            if (p_type == null)
            {
                throw new ArgumentNullException("参数 [p_type] 不能为空!");
            }

            object returnValue = null;

            //要赋的值不为null时
            if (p_object.NotNull())
            {
                Type valueType = null;
                if (p_type.IsValueNullableType(out valueType))
                {
                    //且要赋的值不为空时
                    if (p_object.ConvertToString().NotNullOrEmpty())
                    {
                        //如果是可为空的基础类型，则获取实际的类型，并将值进行转换
                        returnValue = Convert.ChangeType(p_object, valueType);
                    }
                }
                //当类型是值类型
                else if (p_type.IsValueType)
                {
                    //且要赋的值不为空时
                    if (p_object.ConvertToString().NotNullOrEmpty())
                    {
                        if (p_type.Name.EqualsIgnoreCase("GUID"))
                        {
                            returnValue = new Guid(p_object.ConvertToString());
                        }
                        else
                        {
                            //将值转换成指定的类型
                            returnValue = Convert.ChangeType(p_object, p_type);
                        }
                    }
                }
                else
                {
                    //如果是其它类型，则直接进行转换
                    returnValue = Convert.ChangeType(p_object, p_type);
                }
            }

            return returnValue;
        }


        /// <summary>
        /// 反射对象的指定属性信息
        /// </summary>
        /// <remarks>只反射公共属性,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <param name="p_propertyName">属性名称</param>
        /// <returns>如果反射成功，返回属性信息对象，否则返回null。</returns>
        /// <example>
        ///     <code>
        ///     Employee emp = new Employee();
        ///     PropertyInfo propertyInfo = emp.ReflectPropertyInfo("Name");
        ///     </code>
        /// </example>
        public static PropertyInfo ReflectPropertyInfo(this object p_object, string p_propertyName)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.Public;
            PropertyInfo propertyInfo = null;
            if (p_object.NotNull())
            {
                propertyInfo = p_object.GetType().GetProperty(p_propertyName, bindingFlags);
            }
            return propertyInfo;
        }

        /// <summary>
        /// 反射对象的属性信息集合
        /// </summary>
        /// <remarks>只反射公共属性,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <returns>如果反射成功，返回属性信息集合，否则返回null。</returns>
        /// <example>
        ///     <code>
        ///     Employee emp = new Employee();
        ///     PropertyInfo[] propertyInfo = emp.ReflectPropertyInfos();
        ///     </code>
        /// </example>
        public static PropertyInfo[] ReflectPropertyInfos(this object p_object)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.Public;
            PropertyInfo[] propertyInfos = null;
            if (p_object.NotNull())
            {
                propertyInfos = p_object.GetType().GetProperties(bindingFlags);
            }
            return propertyInfos;
        }

        /// <summary>
        /// 通过反射，为对象的指定属性赋值
        /// </summary>
        /// <remarks>只针对公共属性,且不区分大小写。日期类型的数据默认为短日期类型。</remarks>
        /// <param name="p_object">要赋值的对象</param>
        /// <param name="p_propertyName">属性名称</param>
        /// <param name="p_propertyValue">属性值</param>
        /// <example>
        ///     <code>
        ///     Employee emp = new Employee();
        ///     emp.SetPropertyValue("name","wsl");
        ///     </code>
        /// </example>
        public static void SetPropertyValue(this object p_object, string p_propertyName, object p_propertyValue)
        {
            PropertyInfo propertyInfo = p_object.ReflectPropertyInfo(p_propertyName);

            if (propertyInfo.NotNull())
            {
                propertyInfo.SetValue(p_object, p_propertyValue.ConvertToValueType(propertyInfo.PropertyType), null);

                propertyInfo = null;
            }
        }

        /// <summary>
        /// 通过反射，为对象的指定属性赋值
        /// </summary>
        /// <remarks>只针对公共属性,且不区分大小写。日期类型的数据默认为短日期类型。</remarks>
        /// <param name="p_object">要赋值的对象</param>
        /// <param name="p_propertyName">属性名称</param>
        /// <param name="p_propertyValue">属性值</param>
        /// <param name="p_dateFormat">日期格式</param>
        /// <example>
        ///     <code>
        ///     Employee emp = new Employee();        
        ///     emp.SetPropertyValue("onBoardDate",DateTime.New,DateFormat.ShortDate);
        ///     </code>
        /// </example>
        public static void SetPropertyValue(this object p_object, string p_propertyName, object p_propertyValue, DateFormat p_dateFormat)
        {
            PropertyInfo propertyInfo = p_object.ReflectPropertyInfo(p_propertyName);

            if (propertyInfo.NotNull())
            {
                //如果属性值为日期类型，在赋值时，先对其进行格式化,且值不为空时
                if (p_propertyValue.NotNull() && p_propertyValue.GetType().IsDateTimeType())
                {
                    switch (p_dateFormat)
                    {
                        case DateFormat.Default:
                            propertyInfo.SetValue(p_object, DateTime.Parse(p_propertyValue.ToString()).ToString(), null);
                            break;
                        case DateFormat.LongDate:
                            propertyInfo.SetValue(p_object, DateTime.Parse(p_propertyValue.ToString()).ToLongDateString(), null);
                            break;
                        case DateFormat.ShortDate:
                            propertyInfo.SetValue(p_object, DateTime.Parse(p_propertyValue.ToString()).ToShortDateString(), null);
                            break;
                    }
                }
                else
                {
                    propertyInfo.SetValue(p_object, p_propertyValue.ConvertToValueType(propertyInfo.PropertyType), null);
                }
                propertyInfo = null;
            }
        }

        /// <summary>
        /// 通过反射，获取对象的指定属性值
        /// </summary>
        /// <remarks>只针对公共属性,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <param name="p_propertyName">属性名称</param>
        /// <returns>属性值</returns>
        /// <example>
        ///     <code>
        ///     Employee emp = new Employee{Name="wsl"};
        ///     object empName = emp.GetPropertyValue("name");
        ///     </code>
        /// </example>
        public static object GetPropertyValue(this object p_object, string p_propertyName)
        {
            PropertyInfo propertyInfo = p_object.ReflectPropertyInfo(p_propertyName);
            object returnValue = null;
            if (propertyInfo.NotNull())
            {
                returnValue = propertyInfo.GetValue(p_object, null);
            }
            return returnValue;
        }


        /// <summary>
        /// 反射对象的指定字段信息
        /// </summary>
        /// <remarks>只针对私有字段,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <param name="p_fieldName">字段名称</param>
        /// <returns>如果反射成功，返回字段信息对象，否则返回null。</returns>
        public static FieldInfo ReflectFieldInfo(this object p_object, string p_fieldName)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.GetField | BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo fieldInfo = null;
            if (p_object.NotNull())
            {
                fieldInfo = p_object.GetType().GetField(p_fieldName, bindingFlags);
            }
            return fieldInfo;
        }

        /// <summary>
        /// 反射对象的字段信息集合
        /// </summary>
        /// <remarks>只针对私有字段,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <returns>如果反射成功，返回字段信息集合，否则返回null。</returns>
        public static FieldInfo[] ReflectFieldInfos(this object p_object)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.GetField | BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo[] fieldInfos = null;
            if (p_object.NotNull())
            {
                fieldInfos = p_object.GetType().GetFields(bindingFlags);
            }

            return fieldInfos;
        }

        /// <summary>
        /// 通过反射，为对象的指定字段赋值
        /// </summary>
        /// <remarks>只针对字段（私有和公有）,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <param name="p_fieldName">字段名称</param>
        /// <param name="p_fieldValue">字段值</param>
        public static void SetFieldValue(this object p_object, string p_fieldName, object p_fieldValue)
        {
            FieldInfo fieldInfo = p_object.ReflectFieldInfo(p_fieldName);
            if (fieldInfo.NotNull())
            {
                fieldInfo.SetValue(p_object, p_fieldValue.ConvertToValueType(fieldInfo.FieldType));
                fieldInfo = null;
            }
        }

        /// <summary>
        /// 通过反射，为对象的指定字段赋值
        /// </summary>
        /// <remarks>只针对字段（私有和公有）,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <param name="p_fieldName">字段名称</param>
        /// <param name="p_fieldValue">字段值</param>
        /// <param name="p_dateFormat">日期格式</param>
        public static void SetFieldValue(this object p_object, string p_fieldName, object p_fieldValue, DateFormat p_dateFormat)
        {
            FieldInfo fieldInfo = p_object.ReflectFieldInfo(p_fieldName);
            if (fieldInfo.NotNull())
            {
                if (p_fieldValue.NotNull() && p_fieldValue.GetType().IsDateTimeType())
                {
                    switch (p_dateFormat)
                    {
                        case DateFormat.Default:
                            fieldInfo.SetValue(p_object, DateTime.Parse(p_fieldValue.ToString()).ToString());
                            break;
                        case DateFormat.LongDate:
                            fieldInfo.SetValue(p_object, DateTime.Parse(p_fieldValue.ToString()).ToLongDateString());
                            break;
                        case DateFormat.ShortDate:
                            fieldInfo.SetValue(p_object, DateTime.Parse(p_fieldValue.ToString()).ToShortDateString());
                            break;
                    }
                }
                else
                {
                    fieldInfo.SetValue(p_object, p_fieldValue.ConvertToValueType(fieldInfo.FieldType));
                }
            }
            fieldInfo = null;
        }

        /// <summary>
        /// 通过反射，获取对象的指定字段值
        /// </summary>
        /// <remarks>只针对私有字段,且不区分大小写</remarks>
        /// <param name="p_object">要反射的对象</param>
        /// <param name="p_fieldName">字段名称</param>
        /// <returns>字段值</returns>
        public static object GetFieldValue(this object p_object, string p_fieldName)
        {
            FieldInfo fieldInfo = p_object.ReflectFieldInfo(p_fieldName);
            object returnValue = null;
            if (fieldInfo.NotNull())
            {
                returnValue = fieldInfo.GetValue(p_object);
            }
            return returnValue;
        }


        /// <summary>
        /// 根据表达式，解析对象，获得对象的某个值属性
        /// </summary>
        /// <remarks>针对值类型或String类型的属性</remarks>
        /// <param name="p_object">要解析的对象</param>
        /// <param name="p_objectExpression">对象表达式，格式：对象.属性(...)</param>
        /// <param name="p_returnObject">最终得到的属性对象</param>
        /// <returns>最终得到的属性名称</returns>
        /// <example>
        ///     <code>
        ///     Employee emp = new Employee({Name="wsl"});
        ///     object objOut = null;
        ///     //strPropertyName="Name";objOut="wsl";
        ///     string strPropertyName = emp.ParseExpression("emp.Name",out objOut);
        ///     </code>
        /// </example>
        public static string ParseExpression(this object p_object, string p_objectExpression, out object p_returnObject)
        {
            string returnValue = string.Empty;
            PropertyInfo tempPropertyInfo = null;
            Type propertyType = null;
            object tempObject = p_object;
            string[] strArray = p_objectExpression.SplitString(".");

            p_returnObject = p_object;

            for (int i = 1; i < strArray.Length; i++)
            {
                //反射得到指定名称的属性信息
                tempPropertyInfo = tempObject.ReflectPropertyInfo(strArray[i]);
                //获得属性的类型
                propertyType = tempPropertyInfo.PropertyType;
                //如果属性值是值类型或是字符型，则跳出循环，并返当前属性名称
                if (propertyType.IsValueType || propertyType == typeof(string))
                {
                    p_returnObject = tempObject;
                    returnValue = strArray[i];
                    break;
                }
                else
                {
                    //否则，将获得当前属性对象，并传给临时变量
                    tempObject = tempObject.GetPropertyValue(strArray[i]);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 确定是否为可为空的基础类型
        /// </summary>
        /// <remarks>基础类型主要包括int、float、double、bool、char等</remarks>
        /// <param name="p_type">类型</param>
        /// <param name="p_valueType">实际基础类型</param>       
        /// <returns>如果类型是可为空的基础类型，则返回true，否则返回false。</returns>
        public static bool IsValueNullableType(this Type p_type, out Type p_valueType)
        {
            //确定将要转换的类型是否是可为null的基础类型
            bool returnValue = p_type.IsValueNullableType();

            p_valueType = null;

            if (returnValue)
            {
                string typeFullName = p_type.FullName;
                int indexW = typeFullName.IndexOf(",");
                int indexT = typeFullName.IndexOf("[[") + 2;
                //获得实际的基础类型名称
                string currentTypeName = typeFullName.Substring(indexT, indexW - indexT);
                //将对象转换成实际基础类型
                p_valueType = Type.GetType(currentTypeName);
            }

            return returnValue;
        }

        /// <summary>
        /// 确定是否为可为空的基础类型
        /// </summary>        
        /// <param name="p_type">类型</param>        
        /// <example>
        ///     <code>
        ///     int? intT = 1;
        ///     //bl=true;
        ///     bool bl = intT.GetType().IsValueNullableType();
        ///     </code>
        /// </example>
        /// <returns>如果类型是可为空的基础类型，则返回true，否则返回false。</returns>
        public static bool IsValueNullableType(this Type p_type)
        {
            bool returnValue = p_type.Name.EqualsIgnoreCase("Nullable`1");
            return returnValue;
        }

        /// <summary>
        /// 确定是否是日期类型
        /// </summary>
        /// <param name="p_type"></param>
        /// <returns></returns>
        public static bool IsDateTimeType(this Type p_type)
        {
            bool returnValue = false;
            Type valueType = null;
            //如果是可为空类型
            if (p_type.IsValueNullableType(out valueType))
            {
                //且，类型是日期型，
                returnValue = valueType == typeof(DateTime);
            }
            else
            {
                //如果不是可为空类型，则判断当前的类型是否是日期类型
                returnValue = valueType == typeof(DateTime);
            }

            return returnValue;
        }

    }
}