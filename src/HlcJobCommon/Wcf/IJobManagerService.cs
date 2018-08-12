using System.Collections.Generic;
using System.ServiceModel;

namespace HlcJobCommon.Wcf
{
    /// <summary>
    /// 任务管理接口
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IJobManagerCallback))]
    public interface IJobManagerService
    {
        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ManageJob> GetAllJobs();

        /// <summary>
        /// 启用指定任务<paramref name="jobId"/>
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [OperationContract]
        bool EnableJob(string jobId);

        /// <summary>
        /// 禁用指定任务<paramref name="jobId"/>
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [OperationContract]
        bool DisableJob(string jobId);

        /// <summary>
        /// 移除指定任务<paramref name="jobId"/>
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [OperationContract]
        bool RemoveJob(string jobId);

        /// <summary>
        /// 添加任务<paramref name="job"/>
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddJob(ManageJob job);

        /// <summary>
        /// 更新任务<paramref name="job"/>
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateJob(ManageJob job);

        /// <summary>
        /// 执行任务<paramref name="jobId"/>
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [OperationContract]
        bool InvokeJob(string jobId);

        /// <summary>
        /// 获得指定任务<paramref name="jobId"/>的缓存Log
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetChacheLog(string jobId);
    }
    
}