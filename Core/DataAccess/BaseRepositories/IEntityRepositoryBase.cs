using Core.Entity;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.BaseRepositories
{
    public interface IEntityRepositoryBase<T> where T : BaseEntity, new()
    {
        IDataResult<IQueryable<T>> GetAllByFilter(Expression<Func<T, bool>>? filter = null, bool tracking = true);
        IDataResult<T?> Get(Expression<Func<T, bool>> filter);

        IDataResult<T> Add(T entity);
        IDataResult<IEnumerable<T>> AddRange(IEnumerable<T> entities);

        IDataResult<T> Update(T entity);
        IResult Delete(T entity);
        IResult DeleteAllByFilter(Expression<Func<T, bool>> filter);


        Task<IDataResult<T?>> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true);

        Task<IResult> SaveChangesAsync();
        IResult SaveChanges();
    }
}
