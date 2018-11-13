using System;
using System.Threading;

namespace ExeJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("exe 启动了");
            Console.WriteLine($"现在时间是{DateTime.Now}");
            Console.WriteLine($"接下来我睡10秒钟");
            Console.WriteLine(args?.Length);
            for (int i = 0; args != null && i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{i+1} 秒");
            }

            Console.WriteLine("睡醒了");
            
        }
    }
}
