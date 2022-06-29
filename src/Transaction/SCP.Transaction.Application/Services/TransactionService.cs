using MassTransit;
using SCP.Transaction.Application.Models;
using SCP.Transaction.Application.Saga;
using SCP.Transaction.Application.Saga.Events;

namespace SCP.Transaction.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRequestClient<IOnTransactionStarted> _transactionStartClient;
        private readonly IRequestClient<IOnTransactionFinished> _transactionFinishClient;
        private readonly IRequestClient<IOnTransactionUpdated> _transactionUpdatedClient;
        private readonly IRequestClient<IOnTransactionGet> _transactionGetClient;

        public TransactionService(
            IRequestClient<IOnTransactionStarted> transactionStartClient, 
            IRequestClient<IOnTransactionFinished> transactionFinishClient, 
            IRequestClient<IOnTransactionUpdated> transactionUpdatedClient, 
            IRequestClient<IOnTransactionGet> transactionGetClient)
        {
            _transactionStartClient = transactionStartClient;
            _transactionFinishClient = transactionFinishClient;
            _transactionUpdatedClient = transactionUpdatedClient;
            _transactionGetClient = transactionGetClient;
        }

        public async Task<TransactionModel> GetTransaction(Guid transactionId)
        {
            var result = await _transactionGetClient.GetResponse<ITransactionResponse>(new
            {
                TransactionId = transactionId
            });

            return result.Message.Transaction;
        }

        public async Task<TransactionModel> StartTransaction(WorkstationDataModel wsModel)
        {
            var result = await _transactionStartClient.GetResponse<ITransactionResponse>(new
            {
                TransactionId = Guid.NewGuid(),
                WorkstationData = wsModel
            });

            return result.Message.Transaction;
        }

        public async Task<TransactionModel> FinishTransaction(Guid transactionId)
        {
            var result = await _transactionFinishClient.GetResponse<ITransactionResponse>(new
            {
                TransactionId = transactionId
            });

            return result.Message.Transaction;
        }

        public async Task<TransactionModel> AddArticles(Guid transactionId, ICollection<ArticleDataModel> Articles)
        {
            var result = await _transactionUpdatedClient.GetResponse<ITransactionResponse>(new
            {
                TransactionId = transactionId,
                ArticlesToAdd = Articles,
                PaymentsToAdd = Array.Empty<PaymentModel>()
            });

            return result.Message.Transaction;
        }

        public async Task<TransactionModel> AddPayments(Guid transactionId, ICollection<PaymentModel> Payments)
        {
            var result = await _transactionUpdatedClient.GetResponse<ITransactionResponse>(new
            {
                TransactionId = transactionId,
                ArticlesToAdd = Array.Empty<ArticleModel>(),
                PaymentsToAdd = Payments
            });

            return result.Message.Transaction;
        }

        public async Task<TransactionModel> Total(Guid transactionId)
        {
            var result = await _transactionUpdatedClient.GetResponse<ITransactionResponse>(new
            {
                TransactionId = transactionId,
                SwitchToTotal = true
            });

            return result.Message.Transaction;
        } 
    }
}
