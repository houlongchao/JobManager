using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using HlcJobService.Quartz;
using HLC.Common.Extends;
using HLC.Common.IO;
using HLC.Common.Utils;
using NLog;
using Quartz;
using Quartz.Impl;

namespace HlcJobService
{
    /// <summary>
    /// 任务管理
    /// </summary>
    public class JobManager
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();
        private static JobManager _instance;
        private readonly List<ManageJob> _jobs = new List<ManageJob>();
        private IScheduler _scheduler;
        private Dictionary<string, DomainProxy> _domainDict = new Dictionary<string, DomainProxy>();
        private Dictionary<string, Process> _processDict = new Dictionary<string, Process>();

        /// <summary>
        /// 客户端Log处理委托
        /// </summary>
        public Action<string, string> ClientLogHandler { get; set; }

        /// <summary>
        /// 客户端任务状态更新处理委托
        /// </summary>
        public Action<ManageJob> UpdateClientJobHandler { get; set; }
        
        /// <summary>
        /// 所有的Jobs
        /// </summary>
        public List<ManageJob> Jobs => _jobs;

        private JobManager()
        {
            try
            {
                _scheduler = new StdSchedulerFactory().GetScheduler().Result;
                _scheduler.Start();

                _jobs = new List<ManageJob>();

                LoadJobs();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _jobs = new List<ManageJob>();
            }
            
        }

        /// <summary>
        /// JobManager实例
        /// </summary>
        public static JobManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(JobManager))
                    {
                        if (_instance == null)
                        {
                            _instance = new JobManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 从数据文件加载数据
        /// </summary>
        private void LoadJobs()
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(Constant.ManageJobsDataFile);
            var manageJobs = xmlDoc.GetElementsByTagName("ManageJobs");
            var version = manageJobs[0].Attributes?["version"]?.Value.ToInt(-1);
            if (version < 0)    // 如果没有数据版本，则忽略不加载数据
            {
                return;
            }
            var xmlJobs = xmlDoc.GetElementsByTagName("Job");
            foreach (XmlElement xmlJob in xmlJobs)
            {
                switch (version)
                {
                    case 1:
                        _jobs.Add(GetJobFromXml_1(xmlJob));
                        break;
                    case 2:
                        //_jobs.Add(GetJobFromXml_2(xmlJob));
                        break;
                }
            }
        }

        /// <summary>
        /// 从XML获得ManageJob,版本2
        /// </summary>
        /// <param name="xmlJob"></param>
        /// <returns></returns>
        private ManageJob GetJobFromXml_1(XmlElement xmlJob)
        {
            var job = new ManageJob();

            var name = xmlJob.GetAttribute("name");
            job.Name = name;

            var type = xmlJob.GetAttribute("type");
            Enum.TryParse(type, out JobType jobType);
            job.Type = jobType;

            var cron = xmlJob.GetAttribute("cron");
            job.Cron = cron;

            var workPath = xmlJob.GetAttribute("path");
            job.WorkPath = workPath;

            var filePath = xmlJob.GetAttribute("cmd");
            job.Command = filePath;

            var className = xmlJob.GetAttribute("class");
            job.ClassName = className;

            var methodName = xmlJob.GetAttribute("method");
            job.MethodName = methodName;

            var enable = xmlJob.GetAttribute("enable");
            bool.TryParse(enable, out bool jobEnable);
            job.Enable = jobEnable;

            var rank = xmlJob.GetAttribute("rank");
            int.TryParse(rank, out int jobRank);
            job.Rank = jobRank;

            foreach (XmlElement xmlParam in xmlJob.GetElementsByTagName("param"))
            {
                job.Params.Add(xmlParam.Attributes["value"].Value);
            }

            return job;
        }
        
        /// <summary>
        /// 将Job持久化保存到文件中
        /// </summary>
        public void SaveJobs()
        {
            var xmlDoc = new XmlDocument();
            var rootJobs = xmlDoc.CreateElement("ManageJobs");
            rootJobs.SetAttribute("version", "1");
            foreach (var job in Jobs)
            {
                var xmlJob = CreateXmlJob(xmlDoc, job);
                rootJobs.AppendChild(xmlJob);
            }
            xmlDoc.AppendChild(rootJobs);
            xmlDoc.Save(Constant.ManageJobsDataFile);
        }

        /// <summary>
        /// 从ManageJob创建XML
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        private XmlElement CreateXmlJob(XmlDocument xmlDoc, ManageJob job)
        {
            var xmlJob = xmlDoc.CreateElement("Job");
            xmlJob.SetAttribute("id", job.Id);
            xmlJob.SetAttribute("name", job.Name);
            xmlJob.SetAttribute("type", job.Type.ToString());
            xmlJob.SetAttribute("cron", job.Cron);
            xmlJob.SetAttribute("path", job.WorkPath);
            xmlJob.SetAttribute("cmd", job.Command);
            xmlJob.SetAttribute("class", job.ClassName);
            xmlJob.SetAttribute("method", job.MethodName);
            xmlJob.SetAttribute("enable", job.Enable.ToString());
            xmlJob.SetAttribute("rank", job.Rank.ToString());

            foreach (var param in job.Params)
            {
                var xmlParam = xmlDoc.CreateElement("param");
                xmlParam.SetAttribute("value", param);
                xmlJob.AppendChild(xmlParam);
            }

            return xmlJob;
        }

        /// <summary>
        /// 加载所有任务并开始调度
        /// </summary>
        public void LoadAllScheduler()
        {
            _scheduler.Clear();
            Jobs.ForEach(job =>
            {
                if (job.Enable)
                {
                    try
                    {
                        if (job.Cron.Equals(Constant.ServerCron))
                        {
                            InvokeServer(job);
                        }
                        else
                        {
                            var jobDetail = JobBuilder.Create<QuartzJob>()
                                .WithIdentity(job.Id)
                                .UsingJobData(new JobDataMap() { { "job", job } })
                                .Build();

                            var trigger = TriggerBuilder.Create()
                                .StartNow()
                                .WithIdentity(job.Id)
                                .WithCronSchedule(job.Cron)
                                .Build();

                            _scheduler.ScheduleJob(jobDetail, trigger);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Warn(e, $"Job【{job.Name}】Schedule Error");
                    }
                    
                }
            });
        }

        /// <summary>
        /// 更新指定调度
        /// </summary>
        /// <param name="jobId"></param>
        public void UpdateScheduler(string jobId)
        {
            var jobIndex = Jobs.FindIndex(j => j.Id.Equals(jobId));
            if (jobIndex < 0)
            {
                return;
            }

            Jobs[jobIndex].State = JobState.None;

            //删除原有指定调度
            DeleteJob(jobId);

            var job = Jobs[jobIndex];

            if (job == null)
            {
                return;
            }

            try
            {
                if (job.Enable)
                {
                    if (job.Cron.Equals(Constant.ServerCron))
                    {
                        InvokeServer(job);
                    }
                    else
                    {
                        var jobDetail = JobBuilder.Create<QuartzJob>()
                            .WithIdentity(job.Id)
                            .UsingJobData(new JobDataMap() { { "job", job } })
                            .Build();

                        var trigger = TriggerBuilder.Create()
                            .StartNow()
                            .WithIdentity(job.Id)
                            .WithCronSchedule(job.Cron)
                            .Build();

                        _scheduler.ScheduleJob(jobDetail, trigger);
                    }
                }

                UpdateClientJob(Jobs[jobIndex]);
            }
            catch (Exception e)
            {
                _logger.Warn(e, $"Update Job【{job.Name}】Schedule Error");
            }
        }

        /// <summary>
        /// 停止所有任务调度
        /// </summary>
        public void StopScheduler()
        {
            foreach (var job in Jobs)
            {
                DeleteJob(job.Id);
            }

            _scheduler.Clear();
            _scheduler.Shutdown();
        }

        /// <summary>
        /// 执行Job
        /// </summary>
        /// <param name="job"></param>
        public void InvokeJob(ManageJob job)
        {
            switch (job.Type)
            {
                case JobType.DLL:
                    InvokeDllJob(job);
                    break;
                case JobType.EXE:
                    InvokeExeJob(job);
                    break;

                case JobType.CMD:
                    InvokeCmdJob(job);
                    break;
                default:
                    _logger.Warn($"Unkonw Job Type【{job.Type}】");
                    break;
            }
        }

        /// <summary>
        /// 执行Server
        /// </summary>
        /// <param name="job"></param>
        public void InvokeServer(ManageJob job)
        {
            switch (job.Type)
            {
                case JobType.DLL:
                    InvokeDllServer(job);
                    break;
                case JobType.EXE:
                    InvokeExeServer(job);
                    break;
                case JobType.CMD:
                    InvokeCmdServer(job);
                    break;
                default:
                    _logger.Warn($"Unkonw Job Type【{job.Type}】");
                    break;
            }
        }

        /// <summary>
        /// 执行EXE任务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeExeJob(ManageJob job)
        {
            NotifyClientLog(job.Id, "================= EXE Job Running =================");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var args = string.Join(" ", job.Params);

            _logger.Info($"Execute exe Job：{job.Name}[{job.WorkPath}({args})]");
            
            ExecProcessAndWait(job.Id, job.WorkPath, args, Directory.GetParent(job.WorkPath).FullName);

            NotifyClientLog(job.Id, $"================= EXE Finished, Elapsed [{stopwatch.Elapsed:g}] =================");
        }

        /// <summary>
        /// 执行EXE服务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeExeServer(ManageJob job)
        {
            var args = string.Join(" ", job.Params);

            var jobIndex = Jobs.FindIndex(j => j.Id.Equals(job.Id));
            if (jobIndex < 0)
            {
                return;
            }

            Jobs[jobIndex].State = JobState.Normal;
            UpdateClientJob(Jobs[jobIndex]);

            AsyncUtil.Run(() =>
            {
                Thread.Sleep(500);

                Jobs[jobIndex].State = JobState.Normal;
                Jobs[jobIndex].PreviousFireTime = DateTimeOffset.Now;
                UpdateClientJob(Jobs[jobIndex]);

                NotifyClientLog(job.Id, "================= EXE Server Running ==================");

                _logger.Info($"Execute EXE Server Job：{job.Name}[{job.WorkPath} ({args})]");
                
                ExecProcessAndWait(job.Id, job.WorkPath, args, Directory.GetParent(job.WorkPath).FullName);
                
            }, () =>
            {
                Jobs[jobIndex].State = JobState.Complete;

                NotifyClientLog(job.Id, "================= EXE Server Finished ==================");
                UpdateClientJob(Jobs[jobIndex]);
            }, exception =>
            {
                if (exception is AppDomainUnloadedException)
                {
                    NotifyClientLog(job.Id, "================= EXE Server Uninstalled ==================");
                }
                else
                {
                    _logger.Error(exception);
                    NotifyClientLog(job.Id, "EXE Server Error。" + exception.Message);
                    Jobs[jobIndex].State = JobState.Error;

                    NotifyClientLog(job.Id, "================= EXE Server Error ==================");
                    UpdateClientJob(Jobs[jobIndex]);
                }
            });
        }

        /// <summary>
        /// 执行DLL任务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeDllJob(ManageJob job)
        {
            NotifyClientLog(job.Id, "================= DLL Running =================");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var types = job.Params.Select(p => typeof(string)).ToArray();
            var @params = job.Params.ToArray();

            _logger.Info($"Execute DLL Job：{job.Name}[{job.WorkPath}, {job.ClassName},{job.MethodName} ({string.Join(",", @params)})]");

            var writer = new HlcTextWriter();
            writer.WriteHandler += str =>
            {
                NotifyClientLog(job.Id, str);
            };
            var domainProxy = DynamicUtil.LoadDomain(job.WorkPath);
            domainProxy.SetOut(writer);
            var invokeDll = domainProxy.Invoke(job.ClassName, job.MethodName, types, @params);

            if (invokeDll != null)
            {
                NotifyClientLog(job.Id, $"DLL Result:{invokeDll.ToString()}");
            }

            NotifyClientLog(job.Id, $"================= DLL Job Finished, Elapsed [{stopwatch.Elapsed:g}] =================");
        }
        
        /// <summary>
        /// 执行DLL服务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeDllServer(ManageJob job)
        {
            var types = job.Params.Select(p => typeof(string)).ToArray();
            var @params = job.Params.ToArray();

            var jobIndex = Jobs.FindIndex(j => j.Id.Equals(job.Id));
            if (jobIndex < 0)
            {
                return;
            }

            Jobs[jobIndex].State = JobState.Normal;
            UpdateClientJob(Jobs[jobIndex]);

            AsyncUtil.Run(() =>
            {
                Thread.Sleep(500);

                Jobs[jobIndex].State = JobState.Normal;
                Jobs[jobIndex].PreviousFireTime = DateTimeOffset.Now;
                UpdateClientJob(Jobs[jobIndex]);

                NotifyClientLog(job.Id, "================= DLL Server Running ==================");

                _logger.Info($"Execute DLL Server Job：{job.Name}[{job.WorkPath}, {job.ClassName},{job.MethodName} ({string.Join(",", @params)})]");

                var writer = new HlcTextWriter();
                writer.WriteHandler += str =>
                {
                    NotifyClientLog(job.Id, str);
                };

                var domainProxy = DynamicUtil.LoadDomain(job.WorkPath);
                _domainDict[job.Id] = domainProxy;
                domainProxy.SetOut(writer);
                var result = domainProxy.Invoke(job.ClassName, job.MethodName, types, @params);
                if (result != null)
                {
                    NotifyClientLog(job.Id, $"DLL Server Result:{result.ToString()}");
                }

            }, () =>
            {
                //NotifyClientLog(job.Id, "DLL Server 运行完了？？ 完了？？？");
                DynamicUtil.UnloadDomain(_domainDict[job.Id]);
                _domainDict.Remove(job.Id);
                Jobs[jobIndex].State = JobState.Complete;

                NotifyClientLog(job.Id, "================= DLL Server Finished ==================");
                UpdateClientJob(Jobs[jobIndex]);
            }, exception =>
            {
                if (exception is AppDomainUnloadedException)
                {
                    NotifyClientLog(job.Id, "================= DLL Server Uninstalled ==================");
                }
                else
                {
                    _logger.Error(exception);
                    NotifyClientLog(job.Id, "Server Error。" + exception.Message);
                    Jobs[jobIndex].State = JobState.Error;
                    DynamicUtil.UnloadDomain(_domainDict[job.Id]);
                    _domainDict.Remove(job.Id);

                    NotifyClientLog(job.Id, "================= DLL Server Error ==================");
                    UpdateClientJob(Jobs[jobIndex]);
                }
            });
        }
        
        /// <summary>
        /// 执行CMD任务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeCmdJob(ManageJob job)
        {
            NotifyClientLog(job.Id, "================= CMD Running =================");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.Info($"Execute CMD Job：{job.Name}[{job.Command}]");

            var cmd = job.Command;

            var match = Regex.Match(cmd, "((?<exe>(\".+\")|(.+))? (?<args>.+))|(?<exe>(\".+\")|(.+))");
            var exe = match.Groups["exe"].Value;
            var arguments = match.Groups["args"]?.Value ?? "";

            ExecProcessAndWait(job.Id, exe, arguments, job.WorkPath);

            NotifyClientLog(job.Id, $"================= CMD Finished, Elapsed [{stopwatch.Elapsed:g}] =================");
        }
        
        /// <summary>
        /// 执行CMD服务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeCmdServer(ManageJob job)
        {
            var jobIndex = Jobs.FindIndex(j => j.Id.Equals(job.Id));
            if (jobIndex < 0)
            {
                return;
            }

            Jobs[jobIndex].State = JobState.Normal;
            UpdateClientJob(Jobs[jobIndex]);

            AsyncUtil.Run(() =>
            {
                Thread.Sleep(500);

                Jobs[jobIndex].State = JobState.Normal;
                Jobs[jobIndex].PreviousFireTime = DateTimeOffset.Now;
                UpdateClientJob(Jobs[jobIndex]);

                NotifyClientLog(job.Id, "================= CMD Server Running ==================");

                _logger.Info($"Execute CMD Server Job：{job.Name}[{job.WorkPath}]");
                
                var cmd = job.Command;

                var match = Regex.Match(cmd, "((?<exe>(\".+\")|(.+))? (?<args>.+))|(?<exe>(\".+\")|(.+))");
                var exe = match.Groups["exe"].Value;
                var arguments = match.Groups["args"]?.Value??"";

                ExecProcessAndWait(job.Id, exe, arguments, job.WorkPath);
            }, () =>
            {
                if (job.Enable)
                {
                    Jobs[jobIndex].State = JobState.Complete;
                }

                DeleteJob(job.Id);

                NotifyClientLog(job.Id, "================= CMD Server Finished ==================");
                UpdateClientJob(Jobs[jobIndex]);
            }, exception =>
            {
                if (exception is AppDomainUnloadedException)
                {
                    NotifyClientLog(job.Id, "================= CMD Server Uninstalled ==================");
                }
                else
                {
                    _logger.Error(exception);
                    NotifyClientLog(job.Id, "CMD Server Error。" + exception.Message);
                    Jobs[jobIndex].State = JobState.Error;

                    DeleteJob(job.Id);

                    NotifyClientLog(job.Id, "================= CMD Server Error ==================");
                    UpdateClientJob(Jobs[jobIndex]);
                }
            });
        }

        /// <summary>
        /// 执行程序并等待结束
        /// </summary>
        /// <param name="jobId">任务Id</param>
        /// <param name="fileName">程序路径</param>
        /// <param name="arguments">参数</param>
        /// <param name="workPath">工作路径</param>
        private void ExecProcessAndWait(string jobId, string fileName, string arguments, string workPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = workPath;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.OutputDataReceived += (sender, args) =>
            {
                NotifyClientLog(jobId, args.Data);
            };
            process.ErrorDataReceived += (sender, args) =>
            {
                NotifyClientLog(jobId, args.Data);
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            _processDict[jobId] = process;
            process.WaitForExit();
        }

        /// <summary>
        /// 获取所有任务的显示信息
        /// </summary>
        /// <returns></returns>
        public List<ManageJob> GetAllJobShows()
        {
            if (Jobs == null)
            {
                return new List<ManageJob>();
            }

            var jobShows = Jobs.Select(Job2Show).ToList();

            return jobShows;
        }

        /// <summary>
        /// Quartz的Trigger状态转换为任务状态
        /// </summary>
        /// <param name="triggerState">Quartz的Trigger状态</param>
        /// <returns></returns>
        private JobState TriggerState2JobState(TriggerState triggerState)
        {
            switch (triggerState)
            {
                case TriggerState.Normal:
                    return JobState.Normal;
                case TriggerState.Paused:
                    return JobState.Paused;
                case TriggerState.Complete:
                    return JobState.Complete;
                case TriggerState.Error:
                    return JobState.Error;
                case TriggerState.Blocked:
                    return JobState.Blocked;
                case TriggerState.None:
                    return JobState.None;
                default:
                        return JobState.None;
            }
        }

        /// <summary>
        /// 通知客户端显示指定任务<paramref name="jobId"/>的Log<paramref name="message"/>
        /// </summary>
        /// <param name="jobId">任务Id</param>
        /// <param name="message">Log信息</param>
        public void NotifyClientLog(string jobId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            if (message.Length > 500)
            {
                message = message.Substring(0, 497) + "...";
            }

            var log = $"[{DateTime.Now:HH:mm:ss.ffff}] | {message}";
            LogCacheManager.Instance.CacheLog(jobId, log);
            ClientLogHandler?.Invoke(jobId, log);
        }

        /// <summary>
        /// 更新客户端的任务信息
        /// </summary>
        /// <param name="job">要更新的任务</param>
        public void UpdateClientJob(ManageJob job)
        {
            UpdateClientJobHandler?.Invoke(Job2Show(job));
        }

        /// <summary>
        /// 任务信息转换为需要显示的任务信息
        /// </summary>
        /// <param name="job">要显示的任务</param>
        /// <returns></returns>
        private ManageJob Job2Show(ManageJob job)
        {
            if (job.Cron.Equals(Constant.ServerCron))
            {
                job.NextFireTime = null;
                job.State = job.Enable ? job.State : JobState.None;
            }
            else
            {
                var triggerKey = new TriggerKey(job.Id);
                var trigger = _scheduler.GetTrigger(triggerKey).Result;
                var preTime = trigger?.GetPreviousFireTimeUtc();
                if (preTime != null) job.PreviousFireTime = preTime;
                job.NextFireTime = trigger?.GetNextFireTimeUtc();
                job.State = TriggerState2JobState(_scheduler.GetTriggerState(triggerKey).Result);
            }

            return job;
        }

        /// <summary>
        /// 移除指定任务
        /// </summary>
        /// <param name="jobId">任务Id</param>
        public void DeleteJob(string jobId)
        {
            _scheduler.DeleteJob(new JobKey(jobId)).Wait();

            if (_domainDict.ContainsKey(jobId))
            {
                DynamicUtil.UnloadDomain(_domainDict[jobId]);
                _domainDict.Remove(jobId);
                NotifyClientLog(jobId, "Stop Old Job");
            }

            if (_processDict.ContainsKey(jobId))
            {
                using (_processDict[jobId])
                {
                    if (!_processDict[jobId].HasExited)
                    {
                        _processDict[jobId].Kill();
                        
                    }
                }
                _processDict.Remove(jobId);
                NotifyClientLog(jobId, "Stop Old Job");
            }
        }
    }
}