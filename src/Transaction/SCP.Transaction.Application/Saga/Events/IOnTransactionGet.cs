namespace SCP.Transaction.Application.Saga.Events
{
    public interface IOnTransactionGet
    {
        Guid TransactionId { get; set; }
    }
}
