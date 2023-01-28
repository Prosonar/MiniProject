using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Abstract.Service
{
    public interface ICategoryService : ICategoryDal
    {
        IDataResult<List<Product>> GetAllProductByCategory(int categoryId);
        IDataResult<List<Product>> GetAllProductByCategory(Category category);
    }
}
