using Microsoft.AspNetCore.Mvc;
using Module2.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Module2.Controllers
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
    }
}
