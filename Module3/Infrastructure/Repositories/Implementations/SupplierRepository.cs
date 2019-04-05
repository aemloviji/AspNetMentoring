using Microsoft.EntityFrameworkCore;
using Module3.Infrastructure.Repositories.Interfaces;
using Module3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module3.Infrastructure.Repositories.Implementations
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
