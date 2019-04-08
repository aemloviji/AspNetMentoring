using Microsoft.AspNetCore.Mvc;
using Module4.Infrastructure.DAL;
using Module4.Models;
using System.Collections.Generic;
using System.Linq;

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

    }
}