namespace SCP.Transaction.Domain.Exceptions
{
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException()
            : base("Transaction not found.")
        {

        }
    }
}
