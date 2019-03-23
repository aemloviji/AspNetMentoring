using Microsoft.AspNetCore.Mvc;
using Module2.Controllers;
using Module2.Infrastructure.Repositories;
using Module2.Infrastructure.Repositories.Interfaces;
using Module2.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Module2.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task IndexReturnsViewResult()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductsRepository>();
            mockProductRepository.Setup(c => c.ListAsync())
                .ReturnsAsync(GetTestProducsts());
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockProductRepository.Setup(c => c.ListAsync())
                .ReturnsAsync(GetTestProducsts());
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockProductRepository.Setup(c => c.ListAsync())
                .ReturnsAsync(GetTestProducsts());

            var controller = new ProductController(
                mockProductRepository.Object,
                mockSupplierRepository.Object,
                mockCategoryRepository.Object,
                null);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Products>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<Products> GetTestProducsts()
        {
            var products = new List<Products>
            {
                new Products()
                {
                    ProductId = 1,
                    ProductName = "Product1",
                    UnitPrice=3.49m
                },
                new Products()
                {
                    ProductId = 2,
                    ProductName = "Product2",
                    UnitPrice=2.00m
                }
            };
            return products;
        }
    }
}
