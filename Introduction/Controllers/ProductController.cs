using Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Create()
        {
            BuildCreateAndEditFormsViewDataDictionaries(null, null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            BuildCreateAndEditFormsViewDataDictionaries(product.CategoryId, product.SupplierId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            BuildCreateAndEditFormsViewDataDictionaries(products.CategoryId, products.SupplierId);
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (id != products.ProductId)
            {
                return BadRequest();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            BuildCreateAndEditFormsViewDataDictionaries(products.CategoryId, products.SupplierId);
            return View(products);
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        private void BuildCreateAndEditFormsViewDataDictionaries(int? categoryId, int? supplierId)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", supplierId);
        }
    }
}
