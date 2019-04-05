using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module3.Infrastructure.Repositories.Interfaces;
using Module3.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Module3.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.ListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var category = await _categoryRepository.GetByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound($"Category with id {id.Value} not found!");
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("CategoryId,CategoryName,Description")] Categories category,
            IFormFile Picture)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.Picture = await ConvertFormFileToByteArray(Picture);

                    await _categoryRepository.UpdateAsync(category);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    throw;
                }
            }

            return View(category);
        }

        /// Task3: showing image with 2 routers
        public async Task<IActionResult> ShowImage(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var category = await _categoryRepository.GetByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound($"Category with id {id.Value} not found!");
            }

            return View(category);
        }

        private static async Task<byte[]> ConvertFormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
