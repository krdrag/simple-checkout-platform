using MassTransit;
using SCP.MessageBus.Transaction;
using SCP.Transaction.Application.Services;

namespace SCP.Transaction.API.Consumers
{
    public class TransactionListConsumer : IConsumer<GetTransactionListRequest>
    {
        private readonly ITransactionService _transactionService;

        public TransactionListConsumer(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task Consume(ConsumeContext<GetTransactionListRequest> context)
        {
            var msg = context.Message;
            var transactionListForSession = await _transactionService.GetTransactionsForSession(msg.SessionId);

            await context.RespondAsync(new GetTransactionListResponse
            {
                TransactionList = transactionListForSession
            });
        }
    }
}
