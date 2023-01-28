using Business.Abstract.Service;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Concrete.Manager
{
    public class ProductCategoryService : ProductCategoryDal , IProductCategoryService
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductCategoryService(IProductService productService , ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService; 
        }
        public override IDataResult<IQueryable<ProductCategory>> GetAllByFilter(Expression<Func<ProductCategory, bool>>? filter = null, bool tracking = true)
        {
            var result = base.GetAllByFilter(filter, tracking);
            if(!result.Success || result.Data == null)
            {
                return new ErrorDataResult<IQueryable<ProductCategory>>(ResultMessage.Errormessage);
            }

            var data = result.Data.Include(x => x.Product).Include(y => y.Category);
            return new SuccessDataResult<IQueryable<ProductCategory>>(data,ResultMessage.SuccessMessage);
        }

        public override IDataResult<ProductCategory?> Get(Expression<Func<ProductCategory, bool>> filter)
        {
            var productCategory =  base.Get(filter).Data;
            if(productCategory is null)
            {
                return new ErrorDataResult<ProductCategory?>(ResultMessage.Errormessage);
            }

            var product = _productService.Get(x => x.Id == productCategory.Id).Data;
            var category = _categoryService.Get(x => x.Id == productCategory.CategoryId).Data;
            productCategory.Product = product;
            productCategory.Category = category;

            return new SuccessDataResult<ProductCategory?>(productCategory, ResultMessage.SuccessMessage);
        }
    }
}
