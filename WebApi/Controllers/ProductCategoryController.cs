using Business.Abstract.Service;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public IDataResult<List<ProductCategory>> GetCategories()
        {
            return new SuccessDataResult<List<ProductCategory>>(_productCategoryService.GetAllByFilter().Data.ToList());
        }
        [HttpPost]
        public IDataResult<ProductCategory> AddCategory(ProductCategory productCategory)
        {
            var result = _productCategoryService.Add(productCategory);
            _productCategoryService.SaveChanges();
            return result;
        }
    }
}
