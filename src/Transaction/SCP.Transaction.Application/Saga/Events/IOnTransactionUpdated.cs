using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Saga.Events
{
    public interface IOnTransactionUpdated
    {
        Guid TransactionId { get; set; }
        ICollection<ArticleDataModel> ArticlesToAdd { get; set; }
        ICollection<PaymentModel> PaymentsToAdd { get; set; }
        bool SwitchToTotal { get; set; }
    }
}
