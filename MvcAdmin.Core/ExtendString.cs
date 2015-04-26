/* ----------------------------------------------------------
 * @名称     :    字符对象扩展
 * @描述     :    字符串加密解密、编码解码、比较包含、分解等
 * @创建人   :    
 * @创建日期 :    
 * @修改记录 : 
 *    @修改1 :   
 * ----------------------------------------------------------*/

using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace MvcAdmin.Core
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class ExtendString
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="p_inputText">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptStringMD5(this string p_inputText)
        {
            //首先转换数据字符串为Byte数组
            byte[] byteText = Encoding.Unicode.GetBytes(p_inputText.Trim());

            //建立MD5CryptoService实例
            MD5CryptoServiceProvider md5CryptoService = new MD5CryptoServiceProvider();

            //返回加密的数组
            byte[] encrypteByte = md5CryptoService.ComputeHash(byteText);

            string encryptedData = Convert.ToBase64String(encrypteByte);

            byteText = null;
            encrypteByte = null;

            return encryptedData;
        }

        /// <summary>
        /// MD5加密32位
        /// </summary>
        /// <param name="p_inputText">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncyptStringMD5_Normal(this string p_inputText)
        {
            string p_saltKey = "JiLei";
            return EncyptStringMD5_Normal(p_inputText, p_saltKey);
        }

        /// <summary>
        /// Rijndael加密
        /// </summary>
        /// <param name="p_inputText">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString(this string p_inputText)
        {
            return EncryptString(p_inputText, "JiLei");
        }

        /// <summary>
        /// Rijndael解密
        /// </summary>
        /// <param name="p_inputText">要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptString(this string p_inputText)
        {
            return DecryptString(p_inputText, "JiLei");
        }

        /// <summary>
        /// MD5加密32位
        /// </summary>
        /// <param name="p_inputText">要加密的字符串</param>
        /// <param name="p_saltKey">作料</param>
        /// <returns>加密后的字符串</returns>
        public static string EncyptStringMD5_Normal(this string p_inputText, string p_saltKey)
        {
            if (p_saltKey.NotNullOrEmpty())
            {
                string source = p_inputText + p_saltKey;
                byte[] salt = Encoding.UTF8.GetBytes(p_saltKey);
                byte[] password = Encoding.UTF8.GetBytes(p_inputText);
                PasswordDeriveBytes secretKey = new PasswordDeriveBytes(password, salt);
                MD5 md5 = MD5.Create();
                byte[] byteMd5 = md5.ComputeHash(secretKey.GetBytes(256));
                StringBuilder md5StringBuilder = new StringBuilder();
                for (int i = 0; i < byteMd5.Length; i++)
                {
                    md5StringBuilder.Append(byteMd5[i].ToString("X"));
                }
                return md5StringBuilder.ToString();
            }
            else
            {
                return EncyptStringMD5_Normal(p_inputText);
            }
        }

        /// <summary>
        /// Rijndael加密
        /// </summary>
        /// <param name="p_inputText">要加密的字符串</param>
        /// <param name="p_saltKey">作料</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString(this string p_inputText, string p_saltKey)
        {
            #region Rijndael加密
            if (!string.IsNullOrEmpty(p_saltKey))
            {
                // We are now going to create an instance of the 
                // Rijndael class.  
                RijndaelManaged RijndaelCipher = new RijndaelManaged();

                // First we need to turn the input strings into a byte array.
                byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(p_inputText);

                // We are using salt to make it harder to guess our key
                // using a dictionary attack.
                string Salt_Key = p_saltKey;
                byte[] Salt = Encoding.ASCII.GetBytes(Salt_Key.Length.ToString());

                // The (Secret Key) will be generated from the specified 
                // password and salt.
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Salt_Key, Salt);

                // Create a encryptor from the existing SecretKey bytes.
                // We use 32 bytes for the secret key 
                // (the default Rijndael key length is 256 bit = 32 bytes) and
                // then 16 bytes for the IV (initialization vector),
                // (the default Rijndael IV length is 128 bit = 16 bytes)
                ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                // Create a MemoryStream that is going to hold the encrypted bytes 
                MemoryStream memoryStream = new MemoryStream();

                // Create a CryptoStream through which we are going to be processing our data. 
                // CryptoStreamMode.Write means that we are going to be writing data 
                // to the stream and the output will be written in the MemoryStream
                // we have provided. (always use write mode for encryption)
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

                // Start the encryption process.
                cryptoStream.Write(PlainText, 0, PlainText.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memoryStream into a byte array.
                byte[] CipherBytes = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert encrypted data into a base64-encoded string.
                // A common mistake would be to use an Encoding class for that. 
                // It does not work, because not all byte values can be
                // represented by characters. We are going to be using Base64 encoding
                // That is designed exactly for what we are trying to do. 
                string EncryptedData = Convert.ToBase64String(CipherBytes);

                // Return encrypted string.
                return EncryptedData;
            }
            else
            {
                return EncryptString(p_inputText);
            }
            #endregion
        }

        /// <summary>
        /// Rijndael解密
        /// </summary>
        /// <param name="p_inputText">要解密的字符串</param>
        /// <param name="p_saltKey">作料</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptString(this string p_inputText, string p_saltKey)
        {
            #region Rijndael解密
            if (!string.IsNullOrEmpty(p_saltKey))
            {
                try
                {
                    RijndaelManaged RijndaelCipher = new RijndaelManaged();

                    byte[] EncryptedData = Convert.FromBase64String(p_inputText);

                    string Salt_Key = p_saltKey;
                    byte[] Salt = Encoding.ASCII.GetBytes(Salt_Key.Length.ToString());

                    PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Salt_Key, Salt);

                    // Create a decryptor from the existing SecretKey bytes.
                    ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                    MemoryStream memoryStream = new MemoryStream(EncryptedData);

                    // Create a CryptoStream. (always use Read mode for decryption).
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                    // Since at this point we don't know what the size of decrypted data
                    // will be, allocate the buffer long enough to hold EncryptedData;
                    // DecryptedData is never longer than EncryptedData.
                    byte[] PlainText = new byte[EncryptedData.Length];

                    // Start decrypting.
                    int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

                    memoryStream.Close();
                    cryptoStream.Close();
                    // Convert decrypted data into a string.
                    string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
                    // Return decrypted string. 
                    return DecryptedData;
                }
                catch (Exception e)
                {
                    return "解密错误！" + e.Message;
                }
            }
            else
            {
                return DecryptString(p_inputText);
            }
            #endregion
        }


        /// <summary>
        /// 对字符串进行Url编码
        /// </summary>
        /// <param name="p_string">要编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        /// <example>
        ///     <code>
        ///     string strTemp1 = "中国 人";
        ///     //strTemp2 = "%e4%b8%ad%e5%9b%bd+%e4%ba%ba"
        ///     string strTemp2 = strTemp1.UrlEncode();
        ///     </code>
        /// </example>
        public static string UrlEncode(this string p_string)
        {
            return HttpUtility.UrlEncode(p_string);
        }

        /// <summary>
        /// 对字符串进行Url解码
        /// </summary>
        /// <param name="p_string">要解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        /// <example>
        ///     <code>
        ///     string strTemp1 = "%e4%b8%ad%e5%9b%bd+%e4%ba%ba";
        ///     //strTemp2 = "中国 人";
        ///     string strTemp2 = strTemp1.UrlDecode();
        ///     </code>
        /// </example>
        public static string UrlDecode(this string p_string)
        {
            return HttpUtility.UrlDecode(p_string);
        }

        /// <summary>
        /// 对字符串进行Html解码
        /// </summary>
        /// <param name="p_string">要解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        /// <example>
        ///     <code>
        ///     string strTemp1 = "&lt;a href='#'&gt;html&lt;/a&gt;";
        ///     //strTemp2 ="＜a href='#'＞html＜/a＞"
        ///     string strTemp2 = strTemp1.HtmlDecode();        
        ///     </code>
        /// </example>
        public static string HtmlDecode(this string p_string)
        {
            return HttpUtility.HtmlDecode(p_string);
        }

        /// <summary>
        /// 对字符串进行Html编码
        /// </summary>
        /// <param name="p_string">要编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        /// <example>
        ///     <code>
        ///     string strTemp1 = "＜a href='#'＞html＜/a＞";
        ///     //strTemp2 ="&lt;a href='#'&gt;html&lt;/a&gt;"
        ///     string strTemp2 = strTemp1.HtmlEncode();        
        ///     </code>
        /// </example>
        public static string HtmlEncode(this string p_string)
        {
            return HttpUtility.HtmlEncode(p_string);
        }

        /// <summary>
        /// 确认字符串是否为null或空
        /// </summary>
        /// <remarks>当字符串由多个空格组成时，也会被认为是空。</remarks>
        /// <param name="p_string">字符串对象</param>
        /// <returns>如果字符串为空或null，则近回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this string p_string)
        {
            return !string.IsNullOrEmpty(p_string) && p_string.Trim().Length > 0;
        }

        /// <summary>
        /// 确认StringBuilder对象是否为null或空
        /// </summary>
        /// <param name="p_stringBuilder">StringBuilder对象</param>
        /// <returns>如果StringBuilder对象为空或null，则近回false，否则返回true。</returns>
        public static bool NotNullOrEmpty(this StringBuilder p_stringBuilder)
        {
            return p_stringBuilder.NotNull() && p_stringBuilder.Length > 0;
        }

        /// <summary>
        /// 根据指定的分隔符，分解字符串为字符串数组
        /// </summary>
        /// <remarks>
        /// 分解后的字符串数组将不包括转换后为空的元素。
        /// </remarks>
        /// <param name="p_string">待分解的字符串</param>
        /// <param name="p_sepertor">分隔符</param>
        /// <returns>返回分解后的字符串数组</returns>
        /// <example>
        ///     <code>
        ///     string strTemp = "1,2,3,4,5";
        ///     string strArray = strTemp.SplitString(",");
        ///     </code>
        /// </example>
        public static string[] SplitString(this string p_string, string p_sepertor)
        {
            if (!p_sepertor.NotNull())
                throw new ArgumentNullException("参数 [p_sepertor] 不能为null !");

            string[] returnValue = null;
            if (p_string.NotNull())
            {
                returnValue = p_string.Split(
                    new string[] { p_sepertor },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return returnValue;
        }

        /// <summary>
        /// 将字符串转换成int32，如果对象为null,返回-1
        /// </summary>
        /// <param name="p_string"></param>
        /// <returns>返回的字符串</returns>
        public static int ConvertToInt32(this object p_string)
        {
            int returnValue = -1;
            bool bl = false;
            bl = int.TryParse(p_string.ToString(), out returnValue);
            //转换不成功，赋值为-1
            returnValue = bl ? returnValue : -1;

            return returnValue;
        }

        /// <summary>
        /// 将字符串分解后，转换成int数组
        /// </summary>        
        /// <param name="p_string">待分解的字符串</param>
        /// <param name="p_sepertor">分隔符</param>
        /// <param name="p_showCount">显示的数组个数</param>
        /// <returns></returns>
        /// <example>
        ///     string strArray = "1,2,3,4,5,6";
        ///     int []intArray = strArray.ConvertToInt32(",",null);
        ///     //intArray = {1,2,3,4,5,6};
        /// </example>
        public static int[] ConvertToInt32(this string p_string, string p_sepertor, int? p_showCount)
        {
            string[] strArray = p_string.SplitString(p_sepertor);
            int[] returnValue = new int[strArray.Length];
            bool bl = false;
            //初始化
            returnValue.Initialize();
            //将字符串拆成数组后，转换成int数组
            for (int i = 0; i < strArray.Length; i++)
            {
                if (p_showCount != null && i > p_showCount.Value)
                {
                    bl = true;
                    break;
                }
                if (strArray[i].NotNullOrEmpty())
                {
                    returnValue[i] = strArray[i].ConvertToInt32();
                }
            }
            //如果要显示的行数小于分析后的数组
            if (bl)
            {
                returnValue = returnValue.Take(p_showCount.Value).ToArray();
            }
            strArray = null;

            return returnValue;
        }

        /// <summary>
        /// 确定一个字符串是否包含另一个字符串,且忽略大小写。
        /// </summary>        
        /// <param name="p_string">原始字符串</param>
        /// <param name="p_key">要比较的字符串</param>
        /// <returns>如果包含，则返回true，否则返回false。</returns>
        /// <example>
        ///     <code>
        ///     string strTemp ="abcdeDACDS";
        ///     bool bl = strTemp.ContainsIgnoreCase("cd");
        ///     </code>
        /// </example>
        public static bool ContainsIgnoreCase(this string p_string, string p_key)
        {
            bool returnValue = p_string.ToLower().Contains(p_key.ToLower());

            return returnValue;

        }

        /// <summary>
        /// 确定字符串数组中的元素是否存在，比较时忽略元素的大小写
        /// </summary>        
        /// <param name="p_strArray">字符串数组</param>
        /// <param name="p_key">要查找的元素</param>
        /// <returns>如果字符串数组中存在此元素，则返回true，否则返回false。</returns>
        /// <example>
        ///     <code>
        ///     string []strArray = {"a","B","c","D","e"};
        ///     string strKey = "A";
        ///     //返回true
        ///     bool bl = strArray.ContainsIgnoreCase(strKey);
        ///     </code>
        /// </example>
        public static bool ContainsIgnoreCase(this string[] p_strArray, string p_key)
        {
            bool returnValue = false;

            foreach (string item in p_strArray)
            {
                returnValue = item.EqualsIgnoreCase(p_key);
                if (returnValue)
                {
                    break;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 确定字符串列表中的元素是否存在，比较时忽略元素的大小写
        /// </summary>        
        /// <param name="p_lstString">字符串数组</param>
        /// <param name="p_key">要查找的元素</param>
        /// <returns>如果字符串列表中存在此元素，则返回true，否则返回false。</returns>
        /// <example>
        ///     <code>
        ///     List&lt;string&gt; lstString = new List&lt;string&gt;{"a","B","c","D","e"};
        ///     string strKey = "A";
        ///     //返回true
        ///     bool bl = lstString.ContainsIgnoreCase(strKey);
        ///     </code>
        /// </example>
        public static bool ContainsIgnoreCase(this List<string> p_lstString, string p_key)
        {
            bool returnValue = false;

            foreach (string item in p_lstString)
            {
                returnValue = item.EqualsIgnoreCase(p_key);
                if (returnValue)
                {
                    break;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 确定两个字符串是否相等,且比较时忽略大小写。
        /// </summary>
        /// <param name="p_string">原始字符串</param>
        /// <param name="p_otherString">用来比较的字符串</param>
        /// <returns>如果两个字符串相等，则返回true，否则返回false。</returns>
        /// <example>
        ///     <code>
        ///     string strTemp = "abCD";
        ///     bool bl = strTemp.EqualsIgnoreCase("abcd");
        ///     </code>
        /// </example>
        public static bool EqualsIgnoreCase(this string p_string, string p_otherString)
        {
            bool returnValue = p_string.Equals(p_otherString, StringComparison.CurrentCultureIgnoreCase);

            return returnValue;

        }

        #region 防SQL注入
        /// <summary>
        /// 检查参数（防止网页上使用SQL注入式攻击，使用字符检查）
        /// </summary>
        /// <param name="objArgs">要检查的对象数组</param>
        /// <returns>布尔值: true为匹配 | false为未找到</returns>
        public static bool StringCheck(this string objArgs)
        {
            #region 检查参数
            string[] ruleArray = { "'", "\"", "%", ":", "=", "/", "<", ">", "NET USER", "XP_CMDSHELL", "ADD", "EXEC MASTER.DBO.XP_CMDSHELL", "NET LOCALGROUP ADMINISTRATORS", "SELECT", "COUNT", "ASC", "CHAR", "MID", "INSERT", "DELETE FROM", "DROP TABLE", "UPDATE", "TRUNCATE", "FROM" };
            return StringCheck(ruleArray, objArgs);
            #endregion
        }
        /// <summary>
        /// 检查参数（防止网页上使用SQL注入式攻击，使用字符检查）
        /// </summary>
        /// <param name="ruleArray">规则数组</param>
        /// <param name="objArgs">要检查的对象数组</param>
        /// <returns>解密后的字符串</returns>
        public static bool StringCheck(string[] ruleArray, params object[] objArgs)
        {
            #region 检查参数
            if (ruleArray == null || ruleArray.Length <= 0)
            {
                return false;
            }
            //构造正则表达式,例:ruleArray是=号和'号,则正则表达式为 .*[=|'].*  (正则表达式相关内容请见MSDN)
            StringBuilder ruleBuilder = new StringBuilder();
            ruleBuilder.Append(".*(");
            foreach (string rule in ruleArray)
            {
                ruleBuilder.Append(rule);
                ruleBuilder.Append("|");
            }
            ruleBuilder.Remove(ruleBuilder.Length - 1, 1);
            ruleBuilder.Append(").*");
            //从参数中循环对比是否符合规则
            foreach (object objArg in objArgs)
            {
                if (objArg is string)//如果是字符串,直接检查
                {
                    if (Regex.Matches(objArg.ToString(), ruleBuilder.ToString()).Count > 0)
                    {
                        return true;
                    }
                }
                else if (objArg is ICollection)//如果是一个集合,则检查集合内元素是否字符串,是字符串,就进行检查
                {
                    foreach (object objArgIC in (ICollection)objArg)
                    {
                        if (objArgIC is string)
                        {
                            if (Regex.Matches(objArgIC.ToString(), ruleBuilder.ToString()).Count > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            ruleBuilder = null;
            return false;
            #endregion
        }
        /// <summary>
        /// DateTime?类型数据转换短日期字符串
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTime? strDate) {
            try
            {
               return Convert.ToDateTime(strDate).ToShortDateString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion
    }
}
