using Microsoft.EntityFrameworkCore;
using Module2.Infrastructure.Repositories.Interfaces;
using Module2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module2.Infrastructure.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindContext _dbContext;

        public CategoryRepository(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Categories>> ListAsync()
        {
            return _dbContext.Categories.ToListAsync();
        }

        public Task<Categories> GetByIdAsync(int id)
        {
            return _dbContext.Categories
                 .FirstOrDefaultAsync(p => p.CategoryId == id);
        }

        public Task UpdateAsync(Categories category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
