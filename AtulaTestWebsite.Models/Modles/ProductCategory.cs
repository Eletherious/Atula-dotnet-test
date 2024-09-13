namespace AtulaTestWebsite.Models.Modles
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }  // I tried to fix this warning (= new Product();), but it caused issues with the login 

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
