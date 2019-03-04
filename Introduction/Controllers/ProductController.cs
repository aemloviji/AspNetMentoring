using Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.Controllers
{
    public class ProductController : Controller
    {
        private const string ProductsPageSizeConfigurationKey = "ProductsPageSize";

        private readonly NorthwindContext _context;
        private readonly IConfiguration _configuration;

        public ProductController(NorthwindContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var pageSize = _configuration.GetValue<int>(ProductsPageSizeConfigurationKey);
            var northwindContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);

            List<Products> productList;            
            if (pageSize > 0)
            {
                productList = await northwindContext.Take(pageSize).ToListAsync();
            }
            else
            {
                productList = await northwindContext.ToListAsync();
            }

            return View(productList);
        }
    }
}
