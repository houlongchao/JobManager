using System;
using System.IO;

namespace HlcJobManager
{
    public class LogManager
    {
        private static TextWriter _writer;

        public static void SetWriter(TextWriter writer)
        {
            if (_writer != null)
            {
                throw new MethodAccessException("不能重复设置写入器");
            }

            _writer = writer;
        }

        public static void Debug(string message)
        {
            _writer.WriteLine($"[{DateTime.Now:yyyyMMddHHmmssffff}] [Debug] | {message}");
        }

        public static void Info(string message)
        {
            _writer.WriteLine($"[{DateTime.Now:yyyyMMddHHmmssffff}] [Info] | {message}");
        }

        public static void Warn(string message)
        {
            _writer.WriteLine($"[{DateTime.Now:yyyyMMddHHmmssffff}] [Warn ] | {message}");
        }

        public static void Error(string message)
        {
            _writer.WriteLine($"[{DateTime.Now:yyyyMMddHHmmssffff}] [Error] | {message}");
        }
    }
}