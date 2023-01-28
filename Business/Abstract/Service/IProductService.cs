using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Abstract.Service
{
    public interface IProductService : IProductDal
    {
        IDataResult<List<Category>> GetAllCategoryForProduct(int productId);
        IDataResult<List<Category>> GetAllCategoryForProduct(Product product);
        IDataResult<ProductCategory> AddCategoryToProduct(ProductCategory productCategory);
    }
}
