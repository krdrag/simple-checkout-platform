namespace SCP.Transaction.Application.Models
{
    public class ArticleDataModel
    {
        public string Name { get; set; } = string.Empty;
        public string EAN { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ArticleCategory { get; set; } = string.Empty;
    }
}
