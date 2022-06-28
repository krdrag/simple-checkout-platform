namespace SCP.Transaction.Application.Saga.Events
{
    public interface IOnTransactionFinished
    {
        Guid TransactionId { get; set; }
    }
}
