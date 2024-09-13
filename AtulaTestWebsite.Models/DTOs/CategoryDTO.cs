namespace AtulaTestWebsite.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
