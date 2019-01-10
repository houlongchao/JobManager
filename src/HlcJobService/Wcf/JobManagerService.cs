using System;
using System.Collections.Generic;
using System.Linq;
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
                try
                {
                    callback.WriteLog(id, log);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "ClientLogHandler Error.");
                }
            };
            JobManager.Instance.UpdateClientJobHandler = (job) =>
            {
                try
                {
                    callback.JobUpdated(job);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "UpdateClientJobHandler Error.");
                }
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
                    _logger.Warn($"Enable Job【{jobId}】Error，Not Found Job【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"Enable Job Error. Not Found Job.");
                    return false;
                }

                JobManager.Instance.Jobs[jobIndex].Enable = true;

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(jobId);

                _logger.Info($"Enable Job【{jobId}】Success");
                JobManager.Instance.NotifyClientLog(jobId, $"Enable Job Success.");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Enable Job【{jobId}】Error");
                JobManager.Instance.NotifyClientLog(jobId, $"Enable Job Error，{e.Message}");
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
                    _logger.Warn($"Disable Job【{jobId}】Error, Not Found Job【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"Disable Job Error, Not Found Job.");
                    return false;
                }

                JobManager.Instance.Jobs[jobIndex].Enable = false;

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(jobId);

                _logger.Info($"Disable Job【{jobId}】Success");
                JobManager.Instance.NotifyClientLog(jobId, $"Disable Job Success");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Disable Job【{jobId}】Error.");
                JobManager.Instance.NotifyClientLog(jobId, $"Disable Job Error, {e.Message}");
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
                    _logger.Warn($"Remove Job【{jobId}】Error，Not Found Job【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"Remove Job Error, Not Found Job.");
                    return false;
                }

                JobManager.Instance.Jobs.Remove(job);

                JobManager.Instance.SaveJobs();
                JobManager.Instance.DeleteJob(job.Id);

                _logger.Info($"Remove Job【{jobId}】Success.");
                JobManager.Instance.NotifyClientLog(jobId, $"Remove Job Success.");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Remove Job【{jobId}】Error.");
                JobManager.Instance.NotifyClientLog(jobId, $"Remove Job Error，{e.Message}");
                return false;
            }
        }

        public bool AddJob(ManageJob job)
        {
            try
            {
                if (job == null)
                {
                    _logger.Warn($"Add Job Error，Job is null");
                    JobManager.Instance.NotifyClientLog("", $"Add Job Error, Job is null");
                    return false;
                }

                var oldJob = JobManager.Instance.Jobs.Find(j => j.Id.Equals(job.Id));

                if (oldJob != null)
                {
                    _logger.Warn($"Add Job【{job.Id}】Error，Job【{job.Id}】Existed.");
                    JobManager.Instance.NotifyClientLog("", $"Add Job Error, Job Existed.");
                    return false;
                }

                if (JobManager.Instance.Jobs.Count<=0)
                {
                    job.Rank = 1;
                }
                else
                {
                    job.Rank = JobManager.Instance.Jobs.Max(j => j.Rank) + 1;
                }
                
                JobManager.Instance.Jobs.Add(job);

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(job.Id);

                _logger.Info($"Add Job【{job.Id}】Success.");
                JobManager.Instance.NotifyClientLog(job.Id, $"Add Job Success.");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Add Job【{job?.Id}】Error.");
                JobManager.Instance.NotifyClientLog(job?.Id, $"Add Job Error，{e.Message}");
                return false;
            }
        }

        public bool UpdateJob(ManageJob job)
        {
            try
            {
                if (job == null)
                {
                    _logger.Warn($"Update Job Error, Job is null.");
                    JobManager.Instance.NotifyClientLog("", $"Update Job Error, Job is null");
                    return false;
                }

                var jobIndex = JobManager.Instance.Jobs.FindIndex(j => j.Id.Equals(job.Id));

                if (jobIndex < 0)
                {
                    _logger.Warn($"Update Job【{job.Id}】Error, Not Found Job【{job.Id}】");
                    JobManager.Instance.NotifyClientLog(job?.Id, $"Update Job Error, Not Found Job.");
                    return false;
                }

                JobManager.Instance.Jobs[jobIndex] = job;

                JobManager.Instance.SaveJobs();
                JobManager.Instance.UpdateScheduler(job.Id);

                _logger.Info($"Update Job【{job.Id}】Success.");
                JobManager.Instance.NotifyClientLog(job?.Id, $"Update Job Success.");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Update Job【{job?.Id}】Error.");
                JobManager.Instance.NotifyClientLog(job?.Id, $"Update Job Error，{e.Message}");
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
                    _logger.Warn($"Invoke【{jobId}】Error，Not Found Job【{jobId}】");
                    JobManager.Instance.NotifyClientLog(jobId, $"Invoke Job Error, Not Found Job.");
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

                JobManager.Instance.NotifyClientLog(jobId, $"Invoke Job Success.");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Invoke Job【{jobId}】Error.");
                JobManager.Instance.NotifyClientLog(jobId, $"Invoke Job Error，{e.Message}");
                return false;
            }
        }

        public List<string> GetChacheLog(string jobId)
        {
            return LogCacheManager.Instance.GetLog(jobId);
        }

        public bool SwapJobRank(string jobId1, string jobId2)
        {
            try
            {
                var jobIndex1 = JobManager.Instance.Jobs.FindIndex(j => j.Id.Equals(jobId1));
                var jobIndex2 = JobManager.Instance.Jobs.FindIndex(j => j.Id.Equals(jobId2));

                if (jobIndex1 < 0 || jobIndex2 < 0)
                {
                    _logger.Warn($"Swap Job Rank【{jobId1}】【{jobId2}】Error.");
                    JobManager.Instance.NotifyClientLog("", $"Swap Job Rank Error. Not Found Job.");
                    return false;
                }

                var tempRank = JobManager.Instance.Jobs[jobIndex1].Rank;
                JobManager.Instance.Jobs[jobIndex1].Rank = JobManager.Instance.Jobs[jobIndex2].Rank;
                JobManager.Instance.Jobs[jobIndex2].Rank = tempRank;

                JobManager.Instance.SaveJobs();
                
                JobManager.Instance.NotifyClientLog("", $"Swap Job Rank Success.");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Swap Job Rank【{jobId1}】【{jobId2}】Error.");
                JobManager.Instance.NotifyClientLog("", $"Swap Job Rank Error，{e.Message}");
                return false;
            }
        }
    }
}