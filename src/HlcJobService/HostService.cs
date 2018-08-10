using System;
using System.ServiceModel;
using System.Threading;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using HlcJobService.Wcf;
using NLog;

namespace HlcJobService
{
    internal class HostService : IDisposable
    {
        private ILogger m_logger = null;

        private ServiceHost m_service;

        internal HostService()
        {
            m_logger = LogManager.GetCurrentClassLogger(typeof(HostService));

            m_service = new ServiceHost(typeof(JobManagerService), new Uri(Constant.NetNamePipeHost));
            m_service.AddServiceEndpoint(typeof(IJobManagerService), new NetNamedPipeBinding(), "");
            m_service.Opened += (sender, args) => { m_logger.Info("交互服务已打开"); };
            m_service.Closed += (sender, args) => { m_logger.Info("交互服务已关闭"); };
        }

        internal void Start()
        {
            m_logger.Info("服务启动中...");

            m_service.Open();

            JobManager.Instance.LoadAllScheduler();

            m_logger.Info("服务已启动。");
        }

        internal void Stop()
        {
            m_logger.Info("服务停止中...");

            JobManager.Instance.StopScheduler();
            m_service.Close();

            m_logger.Info("服务已停止。");
        }

        public void Dispose()
        {
            ((IDisposable) m_service)?.Dispose();
        }
    }
}