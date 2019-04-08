using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Module4.Infrastructure.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetList(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(int id);

        void Insert(TEntity item);

        void Update(TEntity item);

        void Delete(int id);
    }
}
