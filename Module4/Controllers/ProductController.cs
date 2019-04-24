using System.Collections.Generic;
using System.Linq;
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
            return _unitOfWork.GetRepository<Products>().GetList(includeProperties: "Category")
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Products> Get(int id)
        {
            var product = _unitOfWork.GetRepository<Products>().GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Products>> Create(Products product)
        {
            _unitOfWork.GetRepository<Products>().Insert(product);
            _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Products>> Update(int id, Products product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _unitOfWork.GetRepository<Products>().Update(product);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Products>> Delete(int id)
        {
            var item = _unitOfWork.GetRepository<Products>().GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            _unitOfWork.GetRepository<Products>().Delete(id);
            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}