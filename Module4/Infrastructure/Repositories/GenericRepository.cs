using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module4.Models;

namespace Module4.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly NorthwindContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(NorthwindContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public Task<IEnumerable<TEntity>> ListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(TEntity item)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(TEntity item)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
