using System;
using System.Threading.Tasks;
using HlcJobCommon.Wcf;
using NLog;
using Quartz;

namespace HlcJobService.Quartz
{
    /// <summary>
    /// Quartz任务调度实现
    /// </summary>
    public class QuartzJob : IJob
    {
        private ILogger _logger;

        public QuartzJob()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Task Execute(IJobExecutionContext context)
        {
            ManageJob job = null;
            try
            {
                job = context.JobDetail.JobDataMap["job"] as ManageJob;
            }
            catch (Exception e)
            {
                _logger.Warn(e, "Get Job Info Error");
                JobManager.Instance.NotifyClientLog(job?.Id, $"Get Job Info Error, {e.Message}");
                return null;
            }

            if (job == null)
            {
                _logger.Warn("Execute Job Error. Job is null.");
                JobManager.Instance.NotifyClientLog("", "xecute Job Error. Job is null.");
                return null;
            }

            try
            {
                JobManager.Instance.InvokeJob(job);

                JobManager.Instance.UpdateClientJob(job);
            }
            catch (Exception e)
            {
                _logger.Warn(e, $"Job【{job.Name}】Execute Error");
                JobManager.Instance.NotifyClientLog(job.Id, $"Job【{job.Name}】Execute Error, {e.Message}");
            }

            return Task.FromResult(true);
        }
    }
}