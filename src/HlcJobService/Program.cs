using System;
using System.Configuration;
using System.IO;
using HlcJobCommon;
using NLog;
using Topshelf;

namespace HlcJobService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => LogManager.GetCurrentClassLogger().Fatal(eventArgs.ExceptionObject);

                var srvName = ConfigurationManager.AppSettings["srvname"];
                if (string.IsNullOrWhiteSpace(srvName))
                {
                    srvName = Constant.ServiceName;
                }

                var srvDisplayName = ConfigurationManager.AppSettings["srvdisplayname"];
                if (string.IsNullOrWhiteSpace(srvDisplayName))
                {
                    srvDisplayName = Constant.ServiceName;
                }

                var srvDesc = ConfigurationManager.AppSettings["srvdesc"];
                if (string.IsNullOrWhiteSpace(srvDesc))
                {
                    srvDesc = "HLC任务调度服务";
                }

                HostFactory.Run(config =>
                {
                    config.Service<HostService>(callback =>
                    {
                        callback.ConstructUsing(s => new HostService());
                        callback.WhenStarted(s => s.Start());
                        callback.WhenStopped(s => s.Stop());
                    });
                    config.SetDisplayName(srvDisplayName);
                    config.SetServiceName(srvName);
                    config.SetDescription(srvDesc);
                    config.RunAsLocalSystem();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
