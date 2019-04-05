using Module3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module3.Infrastructure.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Suppliers>> ListAsync();
    }
}
