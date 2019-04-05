using Microsoft.EntityFrameworkCore;
using Module2.Infrastructure.Repositories.Interfaces;
using Module2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module2.Infrastructure.Repositories.Implementations
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindContext _dbContext;

        public SupplierRepository(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Suppliers>> ListAsync()
        {
            return _dbContext.Suppliers.ToListAsync();
        }
    }
}
