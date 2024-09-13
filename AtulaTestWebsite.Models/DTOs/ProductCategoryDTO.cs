namespace AtulaTestWebsite.Models.DTOs
{
    public class ProductCategoryDTO
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
