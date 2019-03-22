using Microsoft.EntityFrameworkCore;
using Module2.Infrastructure.Repositories.Interfaces;
using Module2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module2.Infrastructure.Repositories.Implementations
{
    public class ProductRepository : IProductsRepository
    {
        private readonly NorthwindContext _dbContext;

        public ProductRepository(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Products> GetByIdAsync(int id)
        {
            return _dbContext.Products
                 .Include(p => p.Category)
                 .Include(p => p.Supplier)
                 .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public Task<List<Products>> ListAsync()
        {
            return _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public Task AddAsync(Products product)
        {
            _dbContext.Products.Add(product);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Products product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
