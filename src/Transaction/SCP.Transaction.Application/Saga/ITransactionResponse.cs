using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Saga
{
    public interface ITransactionResponse
    {
        public TransactionModel Transaction { get; }
    }
}
