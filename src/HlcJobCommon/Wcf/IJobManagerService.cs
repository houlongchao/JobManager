using System.Collections.Generic;
using System.ServiceModel;

namespace HlcJobCommon.Wcf
{
    [ServiceContract(CallbackContract = typeof(IJobManagerCallback))]
    public interface IJobManagerService
    {
        [OperationContract]
        List<ManageJob> GetAllJobs();

        [OperationContract]
        bool EnableJob(string jobId);

        [OperationContract]
        bool DisableJob(string jobId);

        [OperationContract]
        bool RemoveJob(string jobId);

        [OperationContract]
        bool AddJob(ManageJob job);

        [OperationContract]
        bool UpdateJob(ManageJob job);

        [OperationContract]
        bool InvokeJob(string jobId);

        [OperationContract]
        List<string> GetChacheLog(string jobId);
    }
    
}