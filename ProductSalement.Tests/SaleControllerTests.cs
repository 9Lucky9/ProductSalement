using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductSalement.Controllers;
using ProductSalement.Models;
using ProductSalement.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductSalement.Tests
{
    public class SaleControllerTests
    {
        private readonly Mock<IRepository<Sale>> _saleRepositoryMock = new Mock<IRepository<Sale>>();
        private readonly Mock<IRepository<Buyer>> _buyerRepositoryMock = new Mock<IRepository<Buyer>>();
        private readonly Mock<IRepository<SalesPoint>> _salesPointRepositoryMock = new Mock<IRepository<SalesPoint>>();
        private readonly Mock<IRepository<Product>> _productRepositoryMock = new Mock<IRepository<Product>>();

        private readonly SaleController _saleController;

        private readonly Fixture _fixture = new Fixture();
        private readonly List<SalesData> _salesData;
        private readonly SalesPoint _salePoint;
        private readonly ProvidedProduct _providedProduct;

        public SaleControllerTests()
        {
            _providedProduct = _fixture.Create<ProvidedProduct>();

            _salePoint = _fixture.Create<SalesPoint>();
            _salesData = _fixture.CreateMany<SalesData>().ToList();

            //Добавление подходящего товара на точку и в список купленных продуктов
            _salePoint.ProvidedProducts.Add(_providedProduct);
            _salesData.Add(_fixture.Build<SalesData>()
                .With(_salesData => _salesData.ProductId, _providedProduct.ProductId)
                .With(_salesData => _salesData.ProductQuantity, _providedProduct.ProductQuantity)
                .Create());

            _salesPointRepositoryMock.Setup(
                repository => repository.Get(It.IsAny<int>()))
                .Returns(_salePoint);
            _saleRepositoryMock.Setup(
                repository => repository.Create(It.IsAny<Sale>()));
            _saleRepositoryMock.Setup(
                repository => repository.Create(It.IsAny<Sale>()));

            _saleController = new SaleController(_saleRepositoryMock.Object,
                _buyerRepositoryMock.Object,
                _salesPointRepositoryMock.Object,
                _productRepositoryMock.Object);

        }

        /// <summary>
        /// Проверяет, что метод Sale возвращает корректный результат
        /// </summary>
        [Fact]
        public void Sale_ReturnRightCode()
        {
            // Arrange
            var fakeSalesPointId = 1;
            var fakeBuyerId = 1;

            //Act
            var actionResult = _saleController.Sale(fakeSalesPointId, fakeBuyerId, _salesData);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        /// <summary>
        /// Проверяет, что метод Sale не возвращает null
        /// </summary>
        [Fact]
        public void Sale_NotNull()
        {
            // Arrange
            var fakeSalesPointId = 1;
            var fakeBuyerId = 1;

            //Act
            var result = _saleController.Sale(fakeSalesPointId, fakeBuyerId, _salesData);

            //Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Проверяет, что количество товара
        /// на точке продажи обновляется
        /// </summary>
        [Fact]
        public void Sale_ProvidedProductsUpdating()
        {
            // Arrange
            var fakeSalesPointId = 1;
            var fakeBuyerId = 1;

            //Act
            var result = _saleController.Sale(fakeSalesPointId, fakeBuyerId, _salesData);

            //Assert
            _salesPointRepositoryMock.Verify(
                repository => repository.Update(It.IsAny<SalesPoint>()), Times.Once);
        }

        /// <summary>
        /// Проверяет, что запись о продаже
        /// создается в базе данных
        /// </summary>
        [Fact]
        public void Sale_SaleCreating()
        {
            // Arrange
            var fakeSalesPointId = 1;
            var fakeBuyerId = 1;

            //Act
            var result = _saleController.Sale(fakeSalesPointId, fakeBuyerId, _salesData);

            //Assert
            _saleRepositoryMock.Verify(
                repository => repository.Create(It.IsAny<Sale>()), Times.Once);
        }


    }
}
