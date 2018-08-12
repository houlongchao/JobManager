using System.ServiceModel;

namespace HlcJobCommon.Wcf
{
    /// <summary>
    /// 任务管理回调接口
    /// </summary>
    public interface IJobManagerCallback
    {
        /// <summary>
        /// 给指定任务<paramref name="jobId"/>写Log信息<paramref name="message"/>
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void WriteLog(string jobId, string message);

        /// <summary>
        /// 更新指定任务信息<paramref name="job"/>
        /// </summary>
        /// <param name="job"></param>
        [OperationContract(IsOneWay = true)]
        void JobUpdated(ManageJob job);
    }
}