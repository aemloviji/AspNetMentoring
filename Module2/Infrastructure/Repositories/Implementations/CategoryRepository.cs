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
    }
}
