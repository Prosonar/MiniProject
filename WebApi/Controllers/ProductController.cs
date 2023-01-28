using Business.Abstract.Service;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService , ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IDataResult<List<Product>> GetProducts()
        {
            var result = _productService.GetAllByFilter();
            if (result.Success)
            {
                return new SuccessDataResult<List<Product>>(result.Data.ToList(), ResultMessage.SuccessMessage);
            }
            return new ErrorDataResult<List<Product>>(ResultMessage.Errormessage);
        }

        [HttpGet("{id}")]
        public IDataResult<List<Category>> GetAllCategoriesForProduct(int productId)
        {
            var result = _productService.GetAllCategoryForProduct(productId);
            if (result.Success)
            {
                return new SuccessDataResult<List<Category>>(result.Data.ToList(), ResultMessage.SuccessMessage);
            }
            return new ErrorDataResult<List<Category>>(ResultMessage.Errormessage);
        }

        [HttpPost]
        public IDataResult<Product> AddProduct(Product product)
        {
            var isAdded = _productService.Add(product);
            var isSaved = _productService.SaveChanges();
            if(isAdded.Success && isSaved.Success)
            {
                return isAdded;
            }
            return new ErrorDataResult<Product>(ResultMessage.Errormessage);
        }
    }
}
