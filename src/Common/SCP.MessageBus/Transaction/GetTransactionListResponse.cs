namespace SCP.MessageBus.Transaction
{
    public class GetTransactionListResponse
    {
        public IEnumerable<string> TransactionList { get; set; } = Array.Empty<string>();
    }
}
