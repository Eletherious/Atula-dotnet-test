using AtulaTestWebsite.DataAccess.IRepository;
using AtulaTestWebsite.Models.ViewModels;
using AtulaTestWebsite.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using AtulaTestWebsite.Models.Modles;
using Microsoft.AspNetCore.Authorization;

namespace AtulaTestWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll(includeProperties: "ProductCategories.Product").ToList();

            List<CategoryDTO> categoryDTOList = categoryList.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            List<CategoryVM> categoryVMList = categoryDTOList.Select(categoryDTO => new CategoryVM
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            }).ToList();

            return View(categoryVMList);
        }

        public IActionResult Create(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM, string? returnUrl = null)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var categoryDTO = new CategoryDTO
                {
                    Name = categoryVM.Name
                };

                var newCategory = new Category
                {
                    Name = categoryDTO.Name
                };

                _unitOfWork.Category.Add(newCategory);
                _unitOfWork.Save();

                TempData["success"] = "Category created successfully";
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(categoryVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductCategories.Product");

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDTO
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name
            };

            var categoryVM = new CategoryVM
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };

            return View(categoryVM);
        }

        [HttpPost]
        public IActionResult Edit(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == categoryVM.Id);

                if (categoryFromDb == null)
                {
                    return NotFound();
                }

                var categoryDTO = new CategoryDTO
                {
                    Name = categoryVM.Name
                };

                categoryFromDb.Name = categoryDTO.Name;

                _unitOfWork.Category.Update(categoryFromDb);
                _unitOfWork.Save();

                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(categoryVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id, includeProperties: "ProductCategories.Product");

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDTO
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name
            };

            var categoryVM = new CategoryVM
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };

            return View(categoryVM);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
