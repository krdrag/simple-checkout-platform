using SCP.Common.Models;

namespace SCP.Transaction.Application.Models
{
    public class TransactionModel
    {
        public Guid TransactionId { get; set; }
        public Guid Sessionid { get; set; }
        public string State { get; set; } = string.Empty;
        public DateTime TimeStarted { get; set; }
        public DateTime TimeFinished { get; set; }
        public WorkstationDataModel WorkstationData { get; set; } = new WorkstationDataModel();
        public ICollection<ArticleModel> Articles { get; set; } = Array.Empty<ArticleModel>();
        public ICollection<PaymentModel> Payments { get; set; } = Array.Empty<PaymentModel>();
        public TotalModel Total { get; set; } = new TotalModel() { Total = 0, NetAmount = 0, TaxAmount = 0 };
    }
}
