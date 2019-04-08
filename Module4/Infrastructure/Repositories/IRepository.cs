using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Module4.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> List(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties);

        TEntity GetById(int id);

        void Insert(TEntity item);

        void Update(TEntity item);

        void Delete(int id);
    }
}
