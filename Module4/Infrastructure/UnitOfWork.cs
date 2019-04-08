using System;
using Module4.Models;

namespace Module4.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private NorthwindContext _context;
        private IRepository<Categories> _categoryRepository;
        private IRepository<Products> _productRepository;
        private IRepository<Suppliers> _supplierRepository;

        public UnitOfWork(NorthwindContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<Categories> CategoryRepository =>
            _categoryRepository = _categoryRepository ?? new GenericRepository<Categories>(_context);

        public IRepository<Products> ProductRepository =>
            _productRepository = _productRepository ?? new GenericRepository<Products>(_context);

        public IRepository<Suppliers> SupplierRepository =>
            _supplierRepository = _supplierRepository ?? new GenericRepository<Suppliers>(_context);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
