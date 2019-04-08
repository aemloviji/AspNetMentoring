using System;
using Module4.Models;

namespace Module4.Infrastructure.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private NorthwindContext _context;

        public UnitOfWork(NorthwindContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
