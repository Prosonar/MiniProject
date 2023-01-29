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
        private readonly IProductCategoryService _productCategoryService;

        public ProductService(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IDataResult<ProductCategory> AddCategoryToProduct(ProductCategory productCategory)
        {
            var isThere = _productCategoryService.Get(x => x.ProductId == productCategory.ProductId && x.CategoryId == productCategory.CategoryId).Data;
            if(isThere is not  null)
            {
                return new ErrorDataResult<ProductCategory>(ResultMessage.AlreadyHaveCategory);
            }
            var isAdded = _productCategoryService.Add(productCategory);
            var isSaved = _productCategoryService.SaveChanges();
            if(!isAdded.Success || !isSaved.Success)
            {
                return new ErrorDataResult<ProductCategory>(ResultMessage.Errormessage);
            }
            return new SuccessDataResult<ProductCategory>(isAdded.Data, ResultMessage.SuccessMessage);
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
