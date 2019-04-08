using System;
using Module4.Models;

namespace Module4.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Categories> CategoryRepository { get; }
        IRepository<Products> ProductRepository { get; }
        IRepository<Suppliers> SupplierRepository { get; }

        void SaveChanges();
    }
}
