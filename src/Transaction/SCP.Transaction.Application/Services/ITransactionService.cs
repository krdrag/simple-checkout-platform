using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Services
{
    /// <summary>
    /// Service used for handling transactions.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Get transaction from Saga with specific id
        /// </summary>
        /// <param name="transactionId">Id of transaction</param>
        /// <returns>Transaction object</returns>
        Task<TransactionModel?> GetTransaction(Guid transactionId);

        /// <summary>
        /// Get list of transactions opened for provided session
        /// </summary>
        /// <param name="sessionId">Id of session</param>
        /// <returns>List of transaction guids</returns>
        Task<IEnumerable<string>> GetTransactionsForSession(Guid sessionId);

        /// <summary>
        /// Remove tracking of all transactions assigned to provided session (transactions are not removed from redis but no longer are tracked by session)
        /// </summary>
        /// <param name="id">Session id</param>
        /// <returns></returns>
        Task ClearTransactionList(Guid sessionId);

        /// <summary>
        /// Start transaction a new transaction
        /// </summary>
        /// <param name="wsModel">Id of session under which this transaction will be created</param>
        /// <returns>Transaction object</returns>
        Task<TransactionModel> StartTransaction(Guid sessionId);

        /// <summary>
        /// Finish and remove transaction
        /// </summary>
        /// <param name="transactionId">Id of transaction</param>
        /// <returns>Transaction object</returns>
        Task<TransactionModel?> FinishTransaction(Guid transactionId);

        /// <summary>
        /// Add new articles to transaction
        /// </summary>
        /// <param name="transactionId">Id of transaction</param>
        /// <param name="Articles">List of new articles</param>
        /// <returns>Transaction object</returns>
        Task<TransactionModel?> AddArticles(Guid transactionId, ICollection<ArticleDataModel> Articles);

        /// <summary>
        /// Add new payments to transaction
        /// </summary>
        /// <param name="transactionId">Id of transaction</param>
        /// <param name="Payments">List of new payments</param>
        /// <returns>Transaction object</returns>
        Task<TransactionModel?> AddPayments(Guid transactionId, ICollection<PaymentModel> Payments);

        /// <summary>
        /// Switch transaction to total mode
        /// </summary>
        /// <param name="transactionId">Id of transaction</param>
        /// <returns>Transaction object</returns>
        Task<TransactionModel?> Total(Guid transactionId);
    }
}
