using System;
using System.ServiceModel;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using HlcJobService.Wcf;
using NLog;

namespace HlcJobService
{
    /// <summary>
    /// 宿主服务
    /// </summary>
    internal class HostService : IDisposable
    {
        private ILogger m_logger = null;

        private ServiceHost m_service;

        internal HostService()
        {
            m_logger = LogManager.GetCurrentClassLogger(typeof(HostService));

            m_service = new ServiceHost(typeof(JobManagerService), new Uri(Constant.NetNamePipeHost));
            m_service.AddServiceEndpoint(typeof(IJobManagerService), new NetNamedPipeBinding(), "");
            m_service.Opened += (sender, args) => { m_logger.Info("Control Service Opened"); };
            m_service.Closed += (sender, args) => { m_logger.Info("Control Service Closed"); };
        }

        /// <summary>
        /// 启动宿主服务
        /// </summary>
        internal void Start()
        {
            m_logger.Info("Host Service Starting...");

            m_service.Open();

            JobManager.Instance.LoadAllScheduler();

            m_logger.Info("Host Service Started。");
        }

        /// <summary>
        /// 停止宿主服务
        /// </summary>
        internal void Stop()
        {
            m_logger.Info("Host Service Stopping...");

            JobManager.Instance.StopScheduler();
            m_service.Close();

            m_logger.Info("Host Service Stopped。");
        }

        public void Dispose()
        {
            ((IDisposable) m_service)?.Dispose();
        }
    }
}