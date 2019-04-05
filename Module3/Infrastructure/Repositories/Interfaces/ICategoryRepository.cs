using Module3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module3.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Categories>> ListAsync();
        Task<Categories> GetByIdAsync(int id);

        Task UpdateAsync(Categories category);
    }
}
