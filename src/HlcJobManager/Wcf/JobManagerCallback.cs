using System;
using System.ServiceModel;
using HlcJobCommon.Wcf;

namespace HlcJobManager.Wcf
{
    /// <summary>
    /// 任务管理回调
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public class JobManagerCallback : IJobManagerCallback
    {
        public static Action<string,string> WriteLogHandler { get; set; }
        public static Action<ManageJob> UpdateClientJobHander { get; set; }

        public void WriteLog(string jobId, string message)
        {
            WriteLogHandler?.Invoke(jobId, message);
        }

        public void JobUpdated(ManageJob job)
        {
            UpdateClientJobHander?.Invoke(job);
        }
    }
}