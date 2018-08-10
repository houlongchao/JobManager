using System.ServiceModel;

namespace HlcJobCommon.Wcf
{
    public interface IJobManagerCallback
    {
        [OperationContract(IsOneWay = true)]
        void WriteLog(string jobId, string message);

        [OperationContract(IsOneWay = true)]
        void JobUpdated(ManageJob job);
    }
}