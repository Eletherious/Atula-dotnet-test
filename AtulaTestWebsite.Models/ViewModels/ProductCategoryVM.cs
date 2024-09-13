using System.Collections.Generic;
using AtulaTestWebsite.Models.Modles;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtulaTestWebsite.Models.ViewModels
{
    public class ProductCategoryVM
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
