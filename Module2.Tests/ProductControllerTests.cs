using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module2.Controllers;
using Module2.Infrastructure.Repositories.Interfaces;
using Module2.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Module2.Tests
{
    public class ProductControllerTests
    {
        private readonly IConfigurationRoot _configuration;

        public ProductControllerTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string>{
                        { "ProductsPageSize", "0" }
                    })
                .Build();
        }

        [Fact]
        public async Task Index_ReturnsViewResult_AndNonEmptyModel()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductsRepository>();
            mockProductRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestProducsts());

            var controller = new ProductController(
                mockProductRepository.Object,
                null,
                null,
                _configuration);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Products>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Create_ReturnsViewResult_WithPopulatedViewDataValues()
        {
            // Arrange
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSuppliers())
                .Verifiable();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestCategories())
                .Verifiable();

            var controller = new ProductController(
                null,
                mockSupplierRepository.Object,
                mockCategoryRepository.Object,
                null);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            ViewDataDictionary viewData = viewResult.ViewData;
            Assert.True(viewData["CategoryId"] != null);
            Assert.True(viewData["SupplierId"] != null);
            mockSupplierRepository.Verify();
            mockCategoryRepository.Verify();
        }

        [Fact]
        public async Task CreatePost_ReturnsSamePageWithErorrModelState_WhenModelIsInvalid()
        {
            // Arrange
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSuppliers())
                .Verifiable();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestCategories())
                .Verifiable();
            var controller = new ProductController(
                null,
                mockSupplierRepository.Object,
                mockCategoryRepository.Object,
                null);
            controller.ModelState.AddModelError("ProductName", "Required");
            var product = new Products();

            // Act
            var result = await controller.Create(product);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            ViewDataDictionary viewData = viewResult.ViewData;
            Assert.True(viewData["CategoryId"] != null);
            Assert.True(viewData["SupplierId"] != null);
            mockSupplierRepository.Verify();
            mockCategoryRepository.Verify();
        }

        [Fact]
        public async Task CreatePost_ReturnsRedirectAndAddsProduct_WhenModelIsValid()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductsRepository>();
            mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Products>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSuppliers());
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestCategories());
            var controller = new ProductController(
                mockProductRepository.Object,
                mockSupplierRepository.Object,
                mockCategoryRepository.Object,
                null);
            var product = new Products()
            {
                ProductName = "UT_Product",
                UnitPrice = 1.25m
            };

            // Act
            var result = await controller.Create(product);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockProductRepository.Verify();
        }


        [Fact]
        public async Task Edit_ReturnsBadRequest_WhenIdArgumentNotProvided()
        {
            // Arrange
            var controller = new ProductController(null, null, null, null);

            // Act
            var result = await controller.Edit(null);

            // Assert
            var viewResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenProductWithGivenIdNotExists()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductsRepository>();
            mockProductRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(default(Products)))
                .Verifiable();
            var controller = new ProductController(
                mockProductRepository.Object,
                null,
                null,
                null);

            // Act
            var result = await controller.Edit(int.MaxValue);

            // Assert
            var viewResult = Assert.IsType<NotFoundObjectResult>(result);
            mockProductRepository.Verify();
        }

        [Fact]
        public void Edit_ReturnsViewResult_WithPopulatedViewDataValues()
        {
            // Arrange
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSuppliers())
                .Verifiable();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestCategories())
                .Verifiable();

            var controller = new ProductController(
                null,
                mockSupplierRepository.Object,
                mockCategoryRepository.Object,
                null);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            ViewDataDictionary viewData = viewResult.ViewData;
            Assert.True(viewData["CategoryId"] != null);
            Assert.True(viewData["SupplierId"] != null);
            mockSupplierRepository.Verify();
            mockCategoryRepository.Verify();
        }

        [Fact]
        public async Task EditPost_ReturnsBadRequest_WhenIdsAreDifferentInPathAndFormData()
        {
            // Arrange
            var controller = new ProductController(
                null,
                null,
                null,
                null);
            var product = new Products()
            {
                ProductId = 1
            };

            // Act
            var result = await controller.Edit(2, product);

            // Assert
            var viewResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task EditPost_ReshowsSamePageWithErrorModelState_WhenModelIsInvalid()
        {
            // Arrange
            var mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSuppliers())
                .Verifiable();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestCategories())
                .Verifiable();
            var controller = new ProductController(
                null,
                mockSupplierRepository.Object,
                mockCategoryRepository.Object,
                null);
            controller.ModelState.AddModelError("ProductName", "Required");
            var product = new Products() { ProductId = 1 };

            // Act
            var result = await controller.Edit(1, product);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            ViewDataDictionary viewData = viewResult.ViewData;
            Assert.True(viewData["CategoryId"] != null);
            Assert.True(viewData["SupplierId"] != null);
            Assert.Single(controller.ViewData.ModelState);
            mockSupplierRepository.Verify();
            mockCategoryRepository.Verify();
        }

        [Fact]
        public async Task EditPost_ReturnsRedirectAndUpdatesProduct_WhenModelIsValid()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductsRepository>();
            mockProductRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Products>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new ProductController(
                mockProductRepository.Object,
                null,
                null,
                null);
            var product = new Products()
            {
                ProductId = 1,
                ProductName = "UT_Product",
                UnitPrice = 1.25m
            };

            // Act
            var result = await controller.Edit(1, product);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockProductRepository.Verify();
        }


        #region private methods
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

        private List<Suppliers> GetTestSuppliers()
        {
            return
                new List<Suppliers>
                {
                    new Suppliers()
                    {
                        SupplierId = 1,
                        CompanyName = "Company1",
                        ContactName="Elvin",
                        City="Minks"
                    },
                    new Suppliers()
                    {
                        SupplierId = 2,
                        CompanyName = "Company2",
                        ContactName="Telman",
                        City="Baku"
                    }
                };
        }

        private List<Categories> GetTestCategories()
        {
            return
                new List<Categories>
                {
                    new Categories()
                    {
                        CategoryId = 1,
                        CategoryName = "Product1"
                    },
                    new Categories()
                    {
                        CategoryId = 2,
                        CategoryName = "Product2",
                    }
                };
        }
        #endregion
    }
}
