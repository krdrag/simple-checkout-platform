using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Services
{
    public interface ITransactionService
    {
        Task<TransactionModel> GetTransaction(Guid transactionId);
        Task<TransactionModel> StartTransaction(WorkstationDataModel wsModel);
    }
}
