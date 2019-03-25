using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module2.Infrastructure.Repositories.Interfaces;
using Module2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module2.Controllers
{
    public class ProductController : Controller
    {
        private const string ProductsPageSizeConfigurationKey = "ProductsPageSize";

        private readonly IProductsRepository _productsRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;

        public ProductController(
            IProductsRepository productsRepository,
            ISupplierRepository supplierRepository,
            ICategoryRepository categoryRepository,
            IConfiguration configuration)
        {
            _productsRepository = productsRepository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;

            _configuration = configuration;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var productList = await _productsRepository.ListAsync();
            productList = PaginateResult(productList);
            return View(productList);
        }

        public IActionResult Create()
        {
            BuildCreateAndEditFormsViewDataDictionaries(null, null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products product)
        {
            if (ModelState.IsValid)
            {
                await _productsRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            BuildCreateAndEditFormsViewDataDictionaries(product.CategoryId, product.SupplierId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var product = await _productsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound($"Product with id {id.Value} not found!");
            }

            BuildCreateAndEditFormsViewDataDictionaries(product.CategoryId, product.SupplierId);
            return View(product);
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
                    await _productsRepository.UpdateAsync(products);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ProductNotExists(products.ProductId))
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

        #region private methods
        private List<Products> PaginateResult(List<Products> productList)
        {
            var pageSize = _configuration.GetValue<int>(ProductsPageSizeConfigurationKey);
            if (pageSize > 0)
            {
                productList = productList.Take(pageSize).ToList();
            }

            return productList;
        }

        private async Task<bool> ProductNotExists(int id)
        {
            return await _productsRepository.GetByIdAsync(id) == null;
        }

        private void BuildCreateAndEditFormsViewDataDictionaries(int? categoryId, int? supplierId)
        {
            var categoryListRetreiverTask = _categoryRepository.ListAsync();
            var supplierListRetreiverTask = _supplierRepository.ListAsync();
            Task.WaitAll(categoryListRetreiverTask, supplierListRetreiverTask);

            ViewData["CategoryId"] = new SelectList(categoryListRetreiverTask.Result, "CategoryId", "CategoryName", categoryId);
            ViewData["SupplierId"] = new SelectList(supplierListRetreiverTask.Result, "SupplierId", "CompanyName", supplierId);
        }
        #endregion
    }
}
