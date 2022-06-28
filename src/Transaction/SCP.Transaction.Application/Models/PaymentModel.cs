namespace SCP.Transaction.Application.Models
{
    public class PaymentModel
    {
        public int PaymentMediaId { get; set; }
        public decimal Amount { get; set; }
        public int LineNumber { get; set; }
    }
}
