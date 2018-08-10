namespace HlcJobCommon
{
    public class Constant
    {
        /// <summary>
        /// 任务管理客户端与服务器交互的地址
        /// </summary>
        public static readonly string NetNamePipeHost = "net.pipe://127.0.0.1/jobmanager";

        /// <summary>
        /// 任务信息存储文件名
        /// </summary>
        public const string ManageJobsDataFile = "ManageJobs.data";

        /// <summary>
        /// HlcJob服务名称
        /// </summary>
        public const string ServiceName = "HlcJobService";

        /// <summary>
        /// Server Cron
        /// </summary>
        public const string ServerCron = "-";
    }
}