using System.Collections.Generic;
using System.ServiceModel;
using HlcJobCommon;
using HlcJobCommon.Wcf;

namespace HlcJobManager.Wcf
{
    /// <summary>
    /// 任务管理调用代理
    /// </summary>
    public class JobManagerProxy : IJobManagerService
    {
        private ChannelFactory<IJobManagerService> m_jobManagerFactory;

        public JobManagerProxy()
        {
            InstanceContext instanceContext = new InstanceContext(new JobManagerCallback());
            m_jobManagerFactory = new DuplexChannelFactory<IJobManagerService>(instanceContext, new NetNamedPipeBinding(), Constant.NetNamePipeHost);
        }

        public List<ManageJob> GetAllJobs()
        {
            return m_jobManagerFactory.CreateChannel().GetAllJobs();
        }

        public bool EnableJob(string jobId)
        {
            return m_jobManagerFactory.CreateChannel().EnableJob(jobId);
        }

        public bool DisableJob(string jobId)
        {
            return m_jobManagerFactory.CreateChannel().DisableJob(jobId);
        }

        public bool RemoveJob(string jobId)
        {
            return m_jobManagerFactory.CreateChannel().RemoveJob(jobId);
        }

        public bool AddJob(ManageJob job)
        {
            return m_jobManagerFactory.CreateChannel().AddJob(job);
        }

        public bool UpdateJob(ManageJob job)
        {
            return m_jobManagerFactory.CreateChannel().UpdateJob(job);
        }

        public bool InvokeJob(string jobId)
        {
            return m_jobManagerFactory.CreateChannel().InvokeJob(jobId);
        }

        public List<string> GetChacheLog(string jobId)
        {
            return m_jobManagerFactory.CreateChannel().GetChacheLog(jobId);
        }
    }
}