using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module4.Infrastructure.DAL;
using Module4.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Module4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categories>> GetAll()
        {
            return _unitOfWork.GetRepository<Categories>().GetList()
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Categories> Get(int id)
        {
            var category = _unitOfWork.GetRepository<Categories>().GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Categories>> UpdateImage(int id, IFormFile file)
        {
            var category = _unitOfWork.GetRepository<Categories>().GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Picture = await ConvertFormFileToByteArray(file);
            _unitOfWork.SaveChanges();

            return category;
        }

        private async Task<byte[]> ConvertFormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}