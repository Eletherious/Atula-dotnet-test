using AtulaTestWebsite.Models.Modles;

public class ProductDTO
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<int> CategoryIds { get; set; } = new List<int>();

    public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
