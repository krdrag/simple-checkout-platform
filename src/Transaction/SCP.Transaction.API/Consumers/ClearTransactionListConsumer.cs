using MassTransit;
using SCP.MessageBus.Transaction;
using SCP.Transaction.Application.Services;

namespace SCP.Transaction.API.Consumers
{
    public class ClearTransactionListConsumer : IConsumer<ClearTransactionListRequest>
    {
        private readonly ITransactionService _transactionService;

        public ClearTransactionListConsumer(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task Consume(ConsumeContext<ClearTransactionListRequest> context)
        {
            var msg = context.Message;
            await _transactionService.ClearTransactionList(msg.Sessionid);

            await context.RespondAsync(new ClearTransactionListResponse());
        }
    }
}
