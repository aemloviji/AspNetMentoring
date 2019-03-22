using Module2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module2.Infrastructure.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Suppliers>> ListAsync();
    }
}
