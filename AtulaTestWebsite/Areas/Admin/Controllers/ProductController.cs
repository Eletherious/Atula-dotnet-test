using AtulaTestWebsite.DataAccess.IRepository;
using AtulaTestWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AtulaTestWebsite.Models.Modles;
using Microsoft.AspNetCore.Authorization;

namespace AtulaTestWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "ProductCategories.Category").ToList();

            List<ProductDTO> productDTOList = productList.Select(product => new ProductDTO
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                ProductCategories = product.ProductCategories.ToList()
            }).ToList();

            List<ProductVM> productVMList = productDTOList.Select(productDTO => new ProductVM
            {
                Id = productDTO.Id,
                Sku = productDTO.Sku,
                Name = productDTO.Name,
                ProductCategories = productDTO.ProductCategories
            }).ToList();

            return View(productVMList);
        }

        public IActionResult Create()
        {
            var productVM = new ProductVM
            {
                Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            ModelState.Remove("Categories");

            if (ModelState.IsValid)
            {
                var productDTO = new ProductDTO
                {
                    Name = productVM.Name,
                    Sku = productVM.Sku
                };

                var newProduct = new Product
                {
                    Name = productDTO.Name,
                    Sku = productDTO.Sku
                };

                _unitOfWork.Product.Add(newProduct);
                _unitOfWork.Save();

                if (productVM.CategoryId > 0)
                {
                    ProductCategory productCategory = new ProductCategory
                    {
                        ProductId = newProduct.Id,
                        CategoryId = productVM.CategoryId
                    };

                    _unitOfWork.ProductCategory.Add(productCategory);
                    _unitOfWork.Save();
                }

                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            productVM.Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return View(productVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductCategories.Category");

            if (productFromDb == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Sku = productFromDb.Sku,
                CategoryIds = productFromDb.ProductCategories.Select(pc => pc.CategoryId).ToList()
            };

            var productVM = new ProductVM
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Sku = productDTO.Sku,
                SelectedCategoryIds = productDTO.CategoryIds,
                Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM productVM)
        {
            ModelState.Remove("Categories");
            if (ModelState.IsValid)
            {
                var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productVM.Id, includeProperties: "ProductCategories");

                if (productFromDb == null)
                {
                    return NotFound();
                }

                var productDTO = new ProductDTO
                {
                    Id = productFromDb.Id,
                    Name = productVM.Name,
                    Sku = productVM.Sku
                };

                productFromDb.Name = productDTO.Name;
                productFromDb.Sku = productDTO.Sku;

                _unitOfWork.Product.Update(productFromDb);
                _unitOfWork.Save();

                var existingProductCategory = _unitOfWork.ProductCategory.GetFirstOrDefault(pc => pc.ProductId == productFromDb.Id);
                if (existingProductCategory != null)
                {
                    _unitOfWork.ProductCategory.Remove(existingProductCategory);
                    _unitOfWork.Save();
                }

                if (productVM.CategoryId > 0)
                {
                    ProductCategory newProductCategory = new ProductCategory
                    {
                        ProductId = productFromDb.Id,
                        CategoryId = productVM.CategoryId
                    };
                    _unitOfWork.ProductCategory.Add(newProductCategory);
                    _unitOfWork.Save();
                }

                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }

            productVM.Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return View(productVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductCategories");

            if (productFromDb == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Sku = productFromDb.Sku
            };

            var productVM = new ProductVM
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Sku = productDTO.Sku,
                ProductCategories = productFromDb.ProductCategories.ToList()
            };

            return View(productVM);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}