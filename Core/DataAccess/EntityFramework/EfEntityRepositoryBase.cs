using Core.DataAccess.BaseRepositories;
using Core.Entity;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public abstract class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity> where TEntity : BaseEntity, new()
                                                        where TContext : DbContext, new()
    {
        protected TContext context = new TContext();

        public virtual async Task<IDataResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = true)
        {
            try
            {
                var result = await context.Set<TEntity>().FirstOrDefaultAsync(filter);
                if (result is null)
                {
                    return new ErrorDataResult<TEntity?>(ResultMessage.DataNotFound);
                }
                return new SuccessDataResult<TEntity?>(result, ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorDataResult<TEntity?>(ResultMessage.DatabaseError);
            }
        }

        public virtual IDataResult<TEntity?> Get(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var result = context.Set<TEntity>().FirstOrDefault(filter);

                if (result is null)
                {
                    return new ErrorDataResult<TEntity?>(ResultMessage.DataNotFound);
                }
                return new SuccessDataResult<TEntity?>(result, ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorDataResult<TEntity?>(ResultMessage.DatabaseError);
            }
        }

        public virtual IDataResult<IQueryable<TEntity>> GetAllByFilter(Expression<Func<TEntity, bool>>? filter = null, bool tracking = true)
        {
            try
            {
                IQueryable<TEntity> result;
                if (filter is null)
                {
                    if (tracking)
                    {
                        result = context.Set<TEntity>();
                    }
                    else
                    {
                        result = context.Set<TEntity>().AsNoTracking();
                    }
                }
                else
                {
                    if (tracking)
                    {
                        result = context.Set<TEntity>().Where(filter);
                    }
                    else
                    {
                        result = context.Set<TEntity>().AsNoTracking().Where(filter);
                    }
                }
                return new SuccessDataResult<IQueryable<TEntity>>(result, ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorDataResult<IQueryable<TEntity>>(ResultMessage.DatabaseError);
            }
        }

        public virtual IDataResult<TEntity> Add(TEntity entity)
        {
            try
            {
                var entityToAdd = context.Entry(entity);
                entityToAdd.State = EntityState.Added;
                return new SuccessDataResult<TEntity>(entity,ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorDataResult<TEntity>(ResultMessage.DatabaseError);
            }
        }

        public virtual IDataResult<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                context.Set<TEntity>().AddRange(entities);
                return new SuccessDataResult<IEnumerable<TEntity>>(entities, ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorDataResult<IEnumerable<TEntity>>(ResultMessage.DatabaseError);
            }
        }

        public virtual IResult Delete(TEntity entity)
        {
            try
            {
                var entityToDelete = context.Entry(entity);
                entityToDelete.State = EntityState.Deleted;
                return new SuccessResult(ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorResult(ResultMessage.DatabaseError);
            }
        }

        public virtual IResult DeleteAllByFilter(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                context.Set<TEntity>().RemoveRange(context.Set<TEntity>().Where(filter));
                return new SuccessResult(ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorResult(ResultMessage.DatabaseError);
            }

        }

        public virtual IDataResult<TEntity> Update(TEntity entity)
        {
            try
            {
                var entityToUpdate = context.Entry(entity);
                entityToUpdate.State = EntityState.Modified;
                return new SuccessDataResult<TEntity>(entity, ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorDataResult<TEntity>(ResultMessage.DatabaseError);
            }
        }

        public async Task<IResult> SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                context.ChangeTracker.Clear();
                return new SuccessResult(ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorResult(ResultMessage.DatabaseSaveChangesError);
            }
        }

        public IResult SaveChanges()
        {
            try
            {
                context.SaveChanges();
                context.ChangeTracker.Clear();
                return new SuccessResult(ResultMessage.SuccessMessage);
            }
            catch
            {
                return new ErrorResult(ResultMessage.DatabaseSaveChangesError);
            }
        }

    }
}
