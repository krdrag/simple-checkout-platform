using SCP.Common.Models;

namespace SCP.Transaction.Application.Saga.Events
{
    public interface IOnTransactionStarted
    {
        Guid TransactionId { get; set; }
        WorkstationDataModel WorkstationData { get; set; }
    }
}
