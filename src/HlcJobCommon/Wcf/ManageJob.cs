using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HlcJobCommon.Wcf
{
    /// <summary>
    /// 任务
    /// </summary>
    [DataContract]
    public class ManageJob
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 任务名
        /// </summary>
        [DataMember] public string Name { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        [DataMember] public JobType Type { get; set; }

        /// <summary>
        /// 任务调度计划
        /// </summary>
        [DataMember] public string Cron { get; set; }

        /// <summary>
        /// 工作目录或文件路径
        /// </summary>
        [DataMember] public string WorkPath { get; set; }
        
        /// <summary>
        /// cmd命令
        /// </summary>
        [DataMember] public string Command { get; set; }

        /// <summary>
        /// 任务执行的类
        /// <remarks>包含命名空间的全类名</remarks>
        /// </summary>
        [DataMember] public string ClassName { get; set; }

        /// <summary>
        /// 任务执行的方法名
        /// </summary>
        [DataMember] public string MethodName { get; set; }

        /// <summary>
        /// 任务执行时的参数列表
        /// </summary>
        [DataMember] public List<string> Params { get; set; } = new List<string>();

        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember] public bool Enable { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [DataMember] public JobState State { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        [DataMember] public DateTimeOffset? NextFireTime { get; set; }

        /// <summary>
        /// 上次执行时间
        /// </summary>
        [DataMember] public DateTimeOffset? PreviousFireTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember] public int Rank { get; set; }
    }

    /// <summary>
    /// 任务类型
    /// </summary>
    [DataContract]
    public enum JobType
    {
        [EnumMember] DLL = 1,
        [EnumMember] EXE = 2,
        [EnumMember] CMD = 3,
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    [DataContract]
    public enum JobState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [EnumMember] Normal = 0,

        /// <summary>
        /// 暂停
        /// </summary>
        [EnumMember] Paused = 1,

        /// <summary>
        /// 完成
        /// </summary>
        [EnumMember] Complete = 2,

        /// <summary>
        /// 出错
        /// </summary>
        [EnumMember] Error = 3,

        /// <summary>
        /// 锁定
        /// </summary>
        [EnumMember] Blocked = 4,

        /// <summary>
        /// 无
        /// </summary>
        [EnumMember] None = 5,

    }
}