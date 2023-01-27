using Business.Abstract.Service;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IDataResult<List<Category>> GetCategories()
        {
            return new SuccessDataResult<List<Category>>(_categoryService.GetAllByFilter().Data.ToList());
        }
        [HttpPost]
        public IDataResult<Category> AddCategory(Category category)
        {
            return _categoryService.Add(category);
        }
    }
}
