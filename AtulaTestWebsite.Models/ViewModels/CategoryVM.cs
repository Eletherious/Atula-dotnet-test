using System.ComponentModel.DataAnnotations;

namespace AtulaTestWebsite.Models.ViewModels
{
    public class CategoryVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
