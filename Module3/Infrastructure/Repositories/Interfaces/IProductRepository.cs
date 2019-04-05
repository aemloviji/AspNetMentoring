using Module3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module3.Infrastructure.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task<Products> GetByIdAsync(int id);
        Task<List<Products>> ListAsync();
        Task AddAsync(Products product);
        Task UpdateAsync(Products product);
    }
}
