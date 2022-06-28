using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Services
{
    public interface ITransactionService
    {
        Task<TransactionModel> GetTransaction(Guid transactionId);
        Task<TransactionModel> StartTransaction(WorkstationDataModel wsModel);
        Task<TransactionModel> FinishTransaction(Guid transactionId);
        Task<TransactionModel> AddArticles(Guid transactionId, ICollection<ArticleDataModel> Articles);
        Task<TransactionModel> AddPayments(Guid transactionId, ICollection<PaymentModel> Payments);
        Task<TransactionModel> Total(Guid transactionId);
    }
}
