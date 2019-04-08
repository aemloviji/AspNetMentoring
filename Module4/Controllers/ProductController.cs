using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Module4.Infrastructure.DAL;
using Module4.Models;

namespace Module4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Products>> GetAll()
        {
            return null;
            //return _unitOfWork.GetRepository<Products>().List();
        }

    }
}