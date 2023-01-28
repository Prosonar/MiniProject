using Business.Abstract.Service;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;

namespace Business.Concrete.Manager
{
    public class CategoryService : CategoryDal, ICategoryService
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public CategoryService(IProductService productService , IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        public IDataResult<List<Product>> GetAllProductByCategory(int categoryId)
        {
            var productCategories = _productCategoryService.GetAllByFilter(x => x.CategoryId == categoryId);
            if (!productCategories.Success)
            {
                return new ErrorDataResult<List<Product>>(ResultMessage.Errormessage);
            }

            var products = productCategories.Data.Select(x => x.Product).ToList();
            return new SuccessDataResult<List<Product>>(products, ResultMessage.SuccessMessage);
        }

        public IDataResult<List<Product>> GetAllProductByCategory(Category category)
        {
            var productCategories = _productCategoryService.GetAllByFilter(x => x.CategoryId == category.Id);
            if (!productCategories.Success)
            {
                return new ErrorDataResult<List<Product>>(ResultMessage.Errormessage);
            }

            var products = productCategories.Data.Select(x => x.Product).ToList();
            return new SuccessDataResult<List<Product>>(products, ResultMessage.SuccessMessage);
        }
    }
}
