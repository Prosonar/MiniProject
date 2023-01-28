using Business.Abstract.Service;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;

namespace Business.Concrete.Manager
{
    public class ProductService : ProductDal, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductService(ICategoryService categoryService, IProductCategoryService productCategoryService)
        {
            _categoryService = categoryService;
            _productCategoryService = productCategoryService;
        }

        public IDataResult<List<Category>> GetAllCategoryForProduct(int productId)
        {
            var productCategories = _productCategoryService.GetAllByFilter(x => x.ProductId == productId);
            if(!productCategories.Success)
            {
                return new ErrorDataResult<List<Category>>(ResultMessage.Errormessage);
            }

            var categories = productCategories.Data.Select(x => x.Category).ToList();
            return new SuccessDataResult<List<Category>>(categories, ResultMessage.SuccessMessage);
        }

        public IDataResult<List<Category>> GetAllCategoryForProduct(Product product)
        {
            var productCategories = _productCategoryService.GetAllByFilter(x => x.ProductId == product.Id);
            if (!productCategories.Success)
            {
                return new ErrorDataResult<List<Category>>(ResultMessage.Errormessage);
            }

            var categories = productCategories.Data.Select(x => x.Category).ToList();
            return new SuccessDataResult<List<Category>>(categories, ResultMessage.SuccessMessage);
        }
    }
}
