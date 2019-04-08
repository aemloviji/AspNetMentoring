using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module4.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync(int id);
    }
}
