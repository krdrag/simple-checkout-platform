namespace SCP.Transaction.Application.Models
{
    public class ArticleModel
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public ArticleDataModel ArticleData { get; set; } = new ArticleDataModel();
        public int LineNumber { get; set; }
    }
}
