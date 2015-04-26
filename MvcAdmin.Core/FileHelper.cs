using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvcAdmin.Core
{
    public class FileHelper
    {
        public static string ReadText(String path) {
            StreamReader objReader = new StreamReader(path);
            string result = objReader.ReadLine();
            objReader.Close();
            objReader.Dispose();
            return result;
        }
        public static void WriteText(String path, String msg)
        {
            // 创建流，并打开文件
            FileStream fs = new FileStream(path, FileMode.Append);
            // 将要写入文件的字符串转为 Byte[]，便于传入流中写入
            // 其实对文件的操作，不管什么格式，读也就是把数据从流中存入 Byte[] 操作，写呢也就是把数据转为 byte[] 从流中写入
            // 转为 byte[] 后，可用于传输，记得以前做了个C#版QQ，也就是这样实现聊天视屏传文件的，其实C#实现很简单，有兴趣的可以Google下
            Byte[] data = Encoding.UTF8.GetBytes(msg);
            // 写入 byte[] 数据
            fs.Write(data, 0, data.Length);
            // 销毁流
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        // 一下就是一些调用时的重载了
        public static void Log(String msg)
        {
            String sysDrive = Environment.GetEnvironmentVariable("SystemDrive");
            String fileName = "BoocaaLog.txt";
            String path = Path.Combine(sysDrive, fileName);
            msg = DateTime.Now.ToString("yyyy/dd/MM HH:mm:ss") + " | " + msg;
            msg = msg.Replace(Environment.NewLine, " ");
            msg += Environment.NewLine;
            FileHelper.WriteText(path, msg);
        }

        public static void Log(String source, String line, String msg)
        {
            String sysDrive = Environment.GetEnvironmentVariable("SystemDrive");
            String fileName = "BoocaaLog.txt";
            String path = Path.Combine(sysDrive, fileName);
            msg = DateTime.Now.ToString("yyyy/dd/MM HH:mm:ss") + " | " + source + " | " + line + " | " + msg;
            msg = msg.Replace(Environment.NewLine, " ");
            msg += Environment.NewLine;
            FileHelper.WriteText(path, msg);
        }

        public static void Log(Exception ex)
        {
            String sysDrive = Environment.GetEnvironmentVariable("SystemDrive");
            String fileName = "BoocaaLog.txt";
            String path = Path.Combine(sysDrive, fileName);
            String msg = "";
            msg = DateTime.Now.ToString("yyyy/dd/MM HH:mm:ss") + " | " + ex.Source + " | " + ex.TargetSite.ToString() + " | " + ex.Message + " | " + ex.StackTrace + msg;
            msg = msg.Replace(Environment.NewLine, " ");
            msg += Environment.NewLine;
            FileHelper.WriteText(path, msg);
        }
    }
}
