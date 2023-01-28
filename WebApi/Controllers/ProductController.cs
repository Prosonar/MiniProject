using Business.Abstract.Service;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IDataResult<List<Product>> GetCategories()
        {
            return new SuccessDataResult<List<Product>>(_productService.GetAllByFilter().Data.ToList());
        }
        [HttpPost]
        public IDataResult<Product> AddCategory(Product product)
        {
            var result = _productService.Add(product);
            _productService.SaveChanges();
            return result;
        }
    }
}
