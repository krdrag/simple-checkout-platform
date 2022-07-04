namespace SCP.Transaction.Domain.Interfaces
{
    public interface ITransactionListCacheRepository
    {
        Task RegisterTransaction(Guid sessionId, Guid transactionId);
        Task RemoveTransaction(Guid sessionId, Guid transactionId);
        Task<IEnumerable<string>> GetAllTransactionsForSession(Guid sessionId);
    }
}
