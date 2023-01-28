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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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

        [HttpGet("{productId}")]
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

        [HttpPost("add-category")]
        public IDataResult<ProductCategory> AddCategoryToProduct(ProductCategory productCategory)
        {
            var isAdded = _productService.AddCategoryToProduct(productCategory);
            var isSaved = _productService.SaveChanges();
            if(isAdded.Success && !isSaved.Success)
            {
                return isAdded;
            }
            return new ErrorDataResult<ProductCategory>(ResultMessage.Errormessage);
        }
    }
}
