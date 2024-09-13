using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AtulaTestWebsite.Models.Modles;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtulaTestWebsite.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public List<int> SelectedCategoryIds { get; set; } = new List<int>();

        [BindNever]  // This shouldnt be validated
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }

}
