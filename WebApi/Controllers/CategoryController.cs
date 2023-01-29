using Business.Abstract.Service;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
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
            List<Category> categories = new List<Category>();
            categories.Add(new Category { Id = 1, Name = "Elektronik", IsActive = true });
            categories.Add(new Category { Id = 2, Name = "Oyuncak", IsActive = true });
            categories.Add(new Category { Id = 3, Name = "Teknoloji", IsActive = true });
            return new SuccessDataResult<List<Category>>(categories, ResultMessage.SuccessMessage);
            //var result = _categoryService.GetAllByFilter();
            //if (result.Success)
            //{
            //    return new SuccessDataResult<List<Category>>(result.Data.ToList(), ResultMessage.SuccessMessage);
            //}
            //return new ErrorDataResult<List<Category>>(ResultMessage.Errormessage);
        }

        [HttpGet("{categoryId}")]
        public IDataResult<List<Product>> GetAllProductsByCategory(int categoryId)
        {
            var result = _categoryService.GetAllProductByCategory(categoryId);
            if (result.Success)
            {
                return new SuccessDataResult<List<Product>>(result.Data.ToList(), ResultMessage.SuccessMessage);
            }
            return new ErrorDataResult<List<Product>>(ResultMessage.Errormessage);
        }

        [HttpPost]
        public IDataResult<Category> AddCategory(Category category)
        {
            var isAdded =  _categoryService.Add(category);
            var isSaved = _categoryService.SaveChanges();
            if(isAdded.Success && isSaved.Success)
            {
                return isAdded;
            }
            return new ErrorDataResult<Category>(ResultMessage.Errormessage);
        }
    }
}
