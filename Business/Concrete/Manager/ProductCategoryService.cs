﻿using Business.Abstract.Service;
using DataAccess.Concrete.EntityFramework;

namespace Business.Concrete.Manager
{
    public class ProductCategoryService : ProductCategoryDal , IProductCategoryService
    {
    }
}
