namespace SCP.Transaction.Application.Models
{
    public class TotalModel
    {
        public decimal Total { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
