using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using HlcJobCommon;
using HlcJobCommon.Wcf;
using HlcJobService.Quartz;
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
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(Constant.ManageJobsDataFile);
                var xmlJobs = xmlDoc.GetElementsByTagName("Job");
                foreach (XmlElement xmlJob in xmlJobs)
                {
                    var job = GetJobFromXml(xmlJob);
                    _jobs.Add(job);
                }
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
        /// 将Job持久化保存到文件中
        /// </summary>
        public void SaveJobs()
        {
            var xmlDoc = new XmlDocument();
            var rootJobs = xmlDoc.CreateElement("ManageJobs");
            foreach (var job in Jobs)
            {
                var xmlJob = CreateXmlJob(xmlDoc, job);
                rootJobs.AppendChild(xmlJob);
            }
            xmlDoc.AppendChild(rootJobs);
            xmlDoc.Save(Constant.ManageJobsDataFile);
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
                        _logger.Warn(e, $"任务【{job.Name}】调度出错");
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
                _logger.Warn(e, $"任务【{job.Name}】调度更新时出错");
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
                    _logger.Warn($"未知的Job类型【{job.Type}】");
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
                    _logger.Warn($"未知的Job类型【{job.Type}】");
                    break;
            }
        }

        /// <summary>
        /// 执行CMD任务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeCmdJob(ManageJob job)
        {
            NotifyClientLog(job.Id, "================= CMD任务准备运行 =================");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.Info($"执行CMD任务：{job.Name}[{job.FilePath}]");
            
            var jobFilePath = job.FilePath;

            var match = Regex.Match(jobFilePath, "((?<exe>(\".+\")|(.+))? (?<args>.+))|(?<exe>(\".+\")|(.+))");
            var exe = match.Groups["exe"].Value;
            var arguments = match.Groups["args"]?.Value ?? "";

            Process process = new Process();
            process.StartInfo.FileName = exe;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = job.WorkPath;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.OutputDataReceived += (sender, args) =>
            {
                NotifyClientLog(job.Id, args.Data);
            };
            process.ErrorDataReceived += (sender, args) =>
            {
                NotifyClientLog(job.Id, args.Data);
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            NotifyClientLog(job.Id, $"================= CMD任务执行成功, 用时[{stopwatch.Elapsed:g}] =================");
        }

        /// <summary>
        /// 执行EXE任务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeExeJob(ManageJob job)
        {
            NotifyClientLog(job.Id, "================= EXE任务准备运行 =================");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var @params = job.Params.ToArray();

            _logger.Info($"执行EXE任务：{job.Name}[{job.FilePath}({string.Join(",", @params)})]");

            var writer = new HlcTextWriter();
            writer.WriteHandler += str =>
            {
                NotifyClientLog(job.Id, str);
            };
            var domainProxy = DynamicUtil.LoadDomain(job.FilePath);
            domainProxy.SetOut(writer);
            var invokeExe = domainProxy.InvokeEntryPoint(@params);
            
            if (invokeExe != null)
            {
                NotifyClientLog(job.Id, $"EXE任务返回值:{invokeExe.ToString()}");
            }
            
            NotifyClientLog(job.Id, $"================= EXE任务执行成功, 用时[{stopwatch.Elapsed:g}] =================");
        }

        /// <summary>
        /// 执行DLL任务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeDllJob(ManageJob job)
        {
            NotifyClientLog(job.Id, "================= DLL任务准备运行 =================");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var types = job.Params.Select(p => typeof(string)).ToArray();
            var @params = job.Params.ToArray();

            _logger.Info($"执行DLL任务：{job.Name}[{job.FilePath}, {job.ClassName},{job.MethodName} ({string.Join(",", @params)})]");

            var writer = new HlcTextWriter();
            writer.WriteHandler += str =>
            {
                NotifyClientLog(job.Id, str);
            };
            var domainProxy = DynamicUtil.LoadDomain(job.FilePath);
            domainProxy.SetOut(writer);
            var invokeDll = domainProxy.Invoke(job.ClassName, job.MethodName, types, @params);

            if (invokeDll != null)
            {
                NotifyClientLog(job.Id, $"DLL任务返回值:{invokeDll.ToString()}");
            }

            NotifyClientLog(job.Id, $"================= DLL任务执行成功, 用时[{stopwatch.Elapsed:g}] =================");
        }
         
        /// <summary>
        /// 执行EXE服务
        /// </summary>
        /// <param name="job"></param>
        private void InvokeExeServer(ManageJob job)
        {
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

                NotifyClientLog(job.Id, "================= EXE Server准备运行 ==================");

                _logger.Info($"执行DLL Server任务：{job.Name}[{job.FilePath}, {job.ClassName},{job.MethodName} ({string.Join(",", @params)})]");

                var writer = new HlcTextWriter();
                writer.WriteHandler += str =>
                {
                    NotifyClientLog(job.Id, str);
                };
                
                var domainProxy = DynamicUtil.LoadDomain(job.FilePath);
                _domainDict[job.Id] = domainProxy;
                domainProxy.SetOut(writer);
                var result = domainProxy.InvokeEntryPoint(@params);
                if (result != null)
                {
                    NotifyClientLog(job.Id, $"EXE Server 返回值:{result.ToString()}");
                }
            }, () =>
            {
                NotifyClientLog(job.Id, "EXE Server 运行完了？？ 完了？？？");
                DynamicUtil.UnloadDomain(_domainDict[job.Id]);
                _domainDict.Remove(job.Id);
                Jobs[jobIndex].State = JobState.Error;

                NotifyClientLog(job.Id, "================= EXE Server运行结束 ==================");
                UpdateClientJob(Jobs[jobIndex]);
            }, exception =>
            {
                if (exception is AppDomainUnloadedException)
                {
                    NotifyClientLog(job.Id, "================= EXE Server已被卸载 ==================");
                }
                else
                {
                    _logger.Error(exception);
                    NotifyClientLog(job.Id, "EXE Server 出异常了。" + exception.Message);
                    Jobs[jobIndex].State = JobState.Error;
                    DynamicUtil.UnloadDomain(_domainDict[job.Id]);
                    _domainDict.Remove(job.Id);

                    NotifyClientLog(job.Id, "================= EXE Server运行出错 ==================");
                    UpdateClientJob(Jobs[jobIndex]);
                }
            });
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

                NotifyClientLog(job.Id, "================= DLL Server准备运行 ==================");

                _logger.Info($"执行DLL Server任务：{job.Name}[{job.FilePath}, {job.ClassName},{job.MethodName} ({string.Join(",", @params)})]");

                var writer = new HlcTextWriter();
                writer.WriteHandler += str =>
                {
                    NotifyClientLog(job.Id, str);
                };

                var domainProxy = DynamicUtil.LoadDomain(job.FilePath);
                _domainDict[job.Id] = domainProxy;
                domainProxy.SetOut(writer);
                var result = domainProxy.Invoke(job.ClassName, job.MethodName, types, @params);
                if (result != null)
                {
                    NotifyClientLog(job.Id, $"DLL Server 返回值:{result.ToString()}");
                }

            }, () =>
            {
                NotifyClientLog(job.Id, "DLL Server 运行完了？？ 完了？？？");
                DynamicUtil.UnloadDomain(_domainDict[job.Id]);
                _domainDict.Remove(job.Id);
                Jobs[jobIndex].State = JobState.Error;

                NotifyClientLog(job.Id, "================= DLL Server运行结束 ==================");
                UpdateClientJob(Jobs[jobIndex]);
            }, exception =>
            {
                if (exception is AppDomainUnloadedException)
                {
                    NotifyClientLog(job.Id, "================= DLL Server已被卸载 ==================");
                }
                else
                {
                    _logger.Error(exception);
                    NotifyClientLog(job.Id, "Server 出异常了。" + exception.Message);
                    Jobs[jobIndex].State = JobState.Error;
                    DynamicUtil.UnloadDomain(_domainDict[job.Id]);
                    _domainDict.Remove(job.Id);

                    NotifyClientLog(job.Id, "================= DLL Server运行出错 ==================");
                    UpdateClientJob(Jobs[jobIndex]);
                }
            });
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

                NotifyClientLog(job.Id, "================= CMD Server准备运行 ==================");

                _logger.Info($"执行CMD Server任务：{job.Name}[{job.FilePath}]");

                var writer = new HlcTextWriter();
                writer.WriteHandler += str =>
                {
                    NotifyClientLog(job.Id, str);
                };
                
                var jobFilePath = job.FilePath;

                var match = Regex.Match(jobFilePath, "((?<exe>(\".+\")|(.+))? (?<args>.+))|(?<exe>(\".+\")|(.+))");
                var exe = match.Groups["exe"].Value;
                var arguments = match.Groups["args"]?.Value??"";

                Process process = new Process();
                process.StartInfo.FileName = exe;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = job.WorkPath;
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.OutputDataReceived += (sender, args) =>
                {
                    NotifyClientLog(job.Id, args.Data);
                };
                process.ErrorDataReceived += (sender, args) =>
                {
                    NotifyClientLog(job.Id, args.Data);
                };
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                _processDict[job.Id] = process;
                process.WaitForExit();
            }, () =>
            {
                if (job.Enable)
                {
                    NotifyClientLog(job.Id, "CMD Server 运行完了？？ 完了？？？");
                    Jobs[jobIndex].State = JobState.Error;
                }

                DeleteJob(job.Id);

                NotifyClientLog(job.Id, "================= CMD Server运行结束 ==================");
                UpdateClientJob(Jobs[jobIndex]);
            }, exception =>
            {
                if (exception is AppDomainUnloadedException)
                {
                    NotifyClientLog(job.Id, "================= CMD Server已被卸载 ==================");
                }
                else
                {
                    _logger.Error(exception);
                    NotifyClientLog(job.Id, "CMD Server 出异常了。" + exception.Message);
                    Jobs[jobIndex].State = JobState.Error;

                    DeleteJob(job.Id);

                    NotifyClientLog(job.Id, "================= CMD Server运行出错 ==================");
                    UpdateClientJob(Jobs[jobIndex]);
                }
            });
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
            xmlJob.SetAttribute("filePath", job.FilePath);
            xmlJob.SetAttribute("workPath", job.WorkPath);
            xmlJob.SetAttribute("className", job.ClassName);
            xmlJob.SetAttribute("methodName", job.MethodName);
            xmlJob.SetAttribute("enable", job.Enable.ToString());

            foreach (var param in job.Params)
            {
                var xmlParam = xmlDoc.CreateElement("param");
                xmlParam.SetAttribute("value", param);
                xmlJob.AppendChild(xmlParam);
            }

            return xmlJob;
        }

        /// <summary>
        /// 从XML获得ManageJob
        /// </summary>
        /// <param name="xmlJob"></param>
        /// <returns></returns>
        private ManageJob GetJobFromXml(XmlElement xmlJob)
        {
            var job = new ManageJob();

            var name = xmlJob.GetAttribute("name");
            job.Name = name;

            var type = xmlJob.GetAttribute("type");
            Enum.TryParse(type, out JobType jobType);
            job.Type = jobType;

            var cron = xmlJob.GetAttribute("cron");
            job.Cron = cron;

            var filePath = xmlJob.GetAttribute("filePath");
            job.FilePath = filePath;

            var workPath = xmlJob.GetAttribute("workPath");
            job.WorkPath = workPath;

            var className = xmlJob.GetAttribute("className");
            job.ClassName = className;

            var methodName = xmlJob.GetAttribute("methodName");
            job.MethodName = methodName;

            var enable = xmlJob.GetAttribute("enable");
            bool.TryParse(enable, out bool jobEnable);
            job.Enable = jobEnable;
            
            foreach (XmlElement xmlParam in xmlJob.GetElementsByTagName("param"))
            {
                job.Params.Add(xmlParam.Attributes["value"].Value);
            }

            return job;
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
                NotifyClientLog(jobId, "停止原有任务");
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
                NotifyClientLog(jobId, "停止原有任务");
            }
        }
    }
}