using System;
using System.IO;

namespace DllJob
{
    public class DemoClass
    {
        public string WriteFile(string path, string message)
        {
            var dir = new FileInfo(path).DirectoryName;
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(path, message + DateTime.Now);

            return $"Write file {path} SUCCESS";
        }
    }
}
