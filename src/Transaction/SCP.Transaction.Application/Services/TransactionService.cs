using MassTransit;
using SCP.Transaction.Application.Models;
using SCP.Transaction.Application.Saga;
using SCP.Transaction.Application.Saga.Events;

namespace SCP.Transaction.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRequestClient<IOnTransactionStarted> _transactionStartClient;
        private readonly IRequestClient<IOnTransactionGet> _transactionGetClient;

        public TransactionService(IRequestClient<IOnTransactionStarted> transactionStartClient, IRequestClient<IOnTransactionGet> transactionGetClient)
        {
            _transactionStartClient = transactionStartClient;
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
    }
}
