using Module4.Models;

namespace Module4.Infrastructure.DAL
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        void SaveChanges();
    }
}
