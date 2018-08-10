using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HlcJobCommon.Wcf;
using NLog;
using Quartz;

namespace HlcJobService.Quartz
{
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
                _logger.Warn(e, "获取Job信息失败");
                JobManager.Instance.NotifyClientLog(job?.Id, $"获取任务信息失败,{e.Message}");
                return null;
            }

            if (job == null)
            {
                _logger.Warn("执行Job失败，Job为空");
                JobManager.Instance.NotifyClientLog("", $"执行任务失败，任务为null");
                return null;
            }

            try
            {
                JobManager.Instance.InvokeJob(job);

                JobManager.Instance.UpdateClientJob(job);
            }
            catch (Exception e)
            {
                _logger.Warn(e, $"任务【{job.Name}】执行失败");
                JobManager.Instance.NotifyClientLog(job.Id, $"任务【{job.Name}】执行失败,{e.Message}");
            }

            return Task.FromResult(true);
        }
    }
}