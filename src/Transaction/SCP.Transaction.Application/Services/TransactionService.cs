using MassTransit;
using SCP.Common.Exceptions;
using SCP.Common.Models;
using SCP.MessageBus.Session;
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

        private readonly IRequestClient<GetSessionRequest> _sessionClient;

        public TransactionService(
            IRequestClient<IOnTransactionStarted> transactionStartClient, 
            IRequestClient<IOnTransactionFinished> transactionFinishClient, 
            IRequestClient<IOnTransactionUpdated> transactionUpdatedClient, 
            IRequestClient<IOnTransactionGet> transactionGetClient, 
            IRequestClient<GetSessionRequest> sessionClient)
        {
            _transactionStartClient = transactionStartClient;
            _transactionFinishClient = transactionFinishClient;
            _transactionUpdatedClient = transactionUpdatedClient;
            _transactionGetClient = transactionGetClient;
            _sessionClient = sessionClient;
        }

        public async Task<TransactionModel?> GetTransaction(Guid transactionId)
        {
            var (status, error) = await _transactionGetClient.GetResponse<ITransactionResponse, ITransactionNotFoundResponse>(new
            {
                TransactionId = transactionId
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Transaction : null;
        }

        public async Task<TransactionModel> StartTransaction(Guid sessionId)
        {
            var response = await _sessionClient.GetResponse<GetSessionResponse>(new GetSessionRequest
            {
                SessionId = sessionId
            });

            if (response == null || response.Message.Session == null)
                throw new SessionNotFoundException();

            var (status, error) = await _transactionStartClient.GetResponse<ITransactionResponse, ITransactionNotFoundResponse>(new
            {
                TransactionId = Guid.NewGuid(),
                WorkstationData = response.Message.Session.WorkstationData
            });

            return status.Result.Message.Transaction;
        }

        public async Task<TransactionModel?> FinishTransaction(Guid transactionId)
        {
            var (status, error) = await _transactionFinishClient.GetResponse<ITransactionResponse, ITransactionNotFoundResponse>(new
            {
                TransactionId = transactionId
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Transaction : null;
        }

        public async Task<TransactionModel?> AddArticles(Guid transactionId, ICollection<ArticleDataModel> Articles)
        {
            var (status, error) = await _transactionUpdatedClient.GetResponse<ITransactionResponse, ITransactionNotFoundResponse>(new
            {
                TransactionId = transactionId,
                ArticlesToAdd = Articles,
                PaymentsToAdd = Array.Empty<PaymentModel>()
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Transaction : null;
        }

        public async Task<TransactionModel?> AddPayments(Guid transactionId, ICollection<PaymentModel> Payments)
        {
            var (status, error) = await _transactionUpdatedClient.GetResponse<ITransactionResponse, ITransactionNotFoundResponse>(new
            {
                TransactionId = transactionId,
                ArticlesToAdd = Array.Empty<ArticleModel>(),
                PaymentsToAdd = Payments
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Transaction : null;
        }

        public async Task<TransactionModel?> Total(Guid transactionId)
        {
            var (status, error) = await _transactionUpdatedClient.GetResponse<ITransactionResponse, ITransactionNotFoundResponse>(new
            {
                TransactionId = transactionId,
                SwitchToTotal = true
            });

            return status.IsCompletedSuccessfully ? status.Result.Message.Transaction : null;
        }
    }
}
