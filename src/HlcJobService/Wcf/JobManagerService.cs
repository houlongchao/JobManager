using System;
using System.Collections.Generic;
using System.ServiceModel;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using NLog;

namespace HlcJobService.Wcf
{
    /// <summary>
    /// 任务管理操作实现
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, AutomaticSessionShutdown = true)]
    public class JobManagerService : IJobManagerService
    {
        private ILogger _logger;
        
        public JobManagerService()
        {
            _logger = LogManager.GetCurrentClassLogger();

            OperationContext.Current.Channel.Closing += (sender, args) =>
            {
                JobManager.Instance.ClientLogHandler = null;
            };

            IJobManagerCallback callback = OperationContext.Current.GetCallbackChannel<IJobManagerCallback>();
            JobManager.Instance.ClientLogHandler = (id, log) =>
            {
                callback.WriteLog(id, log);
            };
            JobManager.Instance.UpdateClientJobHandler = (job) =>
            {
                callback.JobUpdated(job);
            };
        }
        
        public List<ManageJob> GetAllJobs()
        {
            return JobManager.Instance.GetAllJobShows();
        }

        public bool EnableJob(string jobId)
        {
            try
            {
                var jobIndex = JobManager.Instance.Jobs.FindIndex(j => j.Id.Equals(jobId));

                if (jobIndex < 0)
                {
                    _logger.Warn($"启用任务【{jobId}】失败，未找到任务【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"启用任务失败，未找到任务");
                    return false;
                }

                JobManager.Instance.Jobs[jobIndex].Enable = true;

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(jobId);

                _logger.Info($"启用任务【{jobId}】成功");
                JobManager.Instance.NotifyClientLog(jobId, $"启用任务成功");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"启用任务【{jobId}】出错");
                JobManager.Instance.NotifyClientLog(jobId, $"启用任务出错，{e.Message}");
                return false;
            }
        }

        public bool DisableJob(string jobId)
        {
            try
            {
                var jobIndex = JobManager.Instance.Jobs.FindIndex(j => j.Id.Equals(jobId));
                if (jobIndex < 0)
                {
                    _logger.Warn($"禁用任务【{jobId}】失败, 未找到任务【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"禁用任务失败, 未找到任务");
                    return false;
                }

                JobManager.Instance.Jobs[jobIndex].Enable = false;

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(jobId);

                _logger.Info($"禁用任务【{jobId}】成功");
                JobManager.Instance.NotifyClientLog(jobId, $"禁用任务成功");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"禁用任务【{jobId}】出错");
                JobManager.Instance.NotifyClientLog(jobId, $"禁用任务出错, {e.Message}");
                return false;
            }
        }

        public bool RemoveJob(string jobId)
        {
            ManageJob job = null;
            try
            {
                job = JobManager.Instance.Jobs.Find(j => j.Id.Equals(jobId));

                if (job == null)
                {
                    _logger.Warn($"移除任务【{jobId}】失败，未找到任务【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"移除任务失败，未找到任务");
                    return false;
                }

                JobManager.Instance.Jobs.Remove(job);

                JobManager.Instance.SaveJobs();
                JobManager.Instance.DeleteJob(job.Id);

                _logger.Info($"移除任务【{jobId}】成功");
                JobManager.Instance.NotifyClientLog(jobId, $"移除任务成功");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"移除任务【{jobId}】出错");
                JobManager.Instance.NotifyClientLog(jobId, $"移除任务出错，{e.Message}");
                return false;
            }
        }

        public bool AddJob(ManageJob job)
        {
            try
            {
                if (job == null)
                {
                    _logger.Warn($"添加任务出错，任务为null");
                    JobManager.Instance.NotifyClientLog("", $"添加任务出错，任务为null");
                    return false;
                }

                var oldJob = JobManager.Instance.Jobs.Find(j => j.Id.Equals(job.Id));

                if (oldJob != null)
                {
                    _logger.Warn($"添加任务【{job.Id}】出错，任务【{job.Id}】已存在.");
                    JobManager.Instance.NotifyClientLog("", $"添加任务出错，任务已存在.");
                    return false;
                }

                JobManager.Instance.Jobs.Add(job);

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(job.Id);

                _logger.Info($"添加任务【{job.Id}】成功");
                JobManager.Instance.NotifyClientLog(job.Id, $"添加的任务成功");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"添加任务【{job?.Id}】出错");
                JobManager.Instance.NotifyClientLog(job?.Id, $"添加任务出错，{e.Message}");
                return false;
            }
        }

        public bool UpdateJob(ManageJob job)
        {
            try
            {
                if (job == null)
                {
                    _logger.Warn($"更新任务失败，任务为null");
                    JobManager.Instance.NotifyClientLog("", $"更新任务失败，任务为null");
                    return false;
                }

                var jobIndex = JobManager.Instance.Jobs.FindIndex(j => j.Id.Equals(job.Id));

                if (jobIndex < 0)
                {
                    _logger.Warn($"更新任务【{job.Id}】失败, 未找到需要更新的任务【{job.Id}】");
                    JobManager.Instance.NotifyClientLog(job?.Id, $"更新任务失败, 未找到需要更新的任务");
                    return false;
                }

                JobManager.Instance.Jobs[jobIndex] = job;

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(job.Id);

                _logger.Info($"更新任务【{job.Id}】成功");
                JobManager.Instance.NotifyClientLog(job?.Id, $"更新任务成功");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"更新任务【{job?.Id}】出错");
                JobManager.Instance.NotifyClientLog(job?.Id, $"更新任务出错，{e.Message}");
                return false;
            }
        }

        public bool InvokeJob(string jobId)
        {
            ManageJob job = null;
            try
            {
                job = JobManager.Instance.Jobs.Find(j => j.Id.Equals(jobId));

                if (job == null)
                {
                    _logger.Warn($"执行任务【{jobId}】失败，未找到任务【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"执行任务失败，未找到任务");
                    return false;
                }

                if (job.Cron.Equals(Constant.ServerCron))
                {
                    JobManager.Instance.InvokeServer(job);
                }
                else
                {
                    JobManager.Instance.InvokeJob(job);
                }

                JobManager.Instance.NotifyClientLog(jobId, $"执行任务成功");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"执行任务【{jobId}】出错");
                JobManager.Instance.NotifyClientLog(jobId, $"执行任务出错，{e.Message}");
                return false;
            }
        }

        public List<string> GetChacheLog(string jobId)
        {
            return LogCacheManager.Instance.GetLog(jobId);
        }
    }
}