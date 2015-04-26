using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MvcAdmin.Core
{
   public class DES
    {
        //默认密钥向量      
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>      
        /// DES加密字符串      
        /// </summary>      
        /// <param name="encryptString">待加密的字符串</param>      
        /// <param name="encryptKey">加密密钥,要求为8位</param>      
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>      
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch { return encryptString; }
        }
        /// <summary>      
        /// DES解密字符串      
        /// </summary>      
        /// <param name="decryptString">待解密的字符串</param>      
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>      
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>      
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch { return decryptString; }
        }



        /// <summary>
        /// MD5加密方法
        /// </summary>
        /// <param name="oldpwd">要加密的字符串</param>
        /// <returns>加密之后的字符串</returns>
        public static string GetMD5String(string oldpwd)
        {
            string newPwd = string.Empty;//声明一个字符串来存放加密后的字符串
            byte[] result = Encoding.Default.GetBytes(oldpwd);//把要加密的字符串通过默认编码转换成byte[]类型
            MD5 md5 = new MD5CryptoServiceProvider();//创建一个用于MD5加密的类
            byte[] output = md5.ComputeHash(result);// 对字符串进行加密
            newPwd = BitConverter.ToString(output).Replace("-", "").ToLower();//将加密后的字节数组转成字符串并去掉横杠

            return newPwd;//返回新的加密字符串//发送
        }



    }
}
