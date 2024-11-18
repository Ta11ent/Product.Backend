using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Product.GetProductDetails;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Domain;
using System.Linq.Expressions;


namespace ProductCatalog.UnitTests.UnitTests.Queries.Product
{
    public class GetProductDetailsQueryHandlerTests : BaseTestHandler<IProductRepository>
    {
        private readonly GetProductDetailsQuery _query;
        private readonly Mock<ICurrencyRepository> _currencyRepository;
        private readonly ProductSale _product;
        public GetProductDetailsQueryHandlerTests() {
            _product = new ProductSale().Create(Guid.NewGuid(), Guid.NewGuid(), true);
            _query = new GetProductDetailsQuery()
            {
                CategoryId = Guid.NewGuid(),
                SubCategoryId = _product.SubCategoryId,
                ProductId = _product.ProductId
            };
            _currencyRepository = new();
        }

        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoProduct()
        {
            //Arrange
            _repository.Setup(
              x => x.GetProductByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<Expression<Func<ProductSale, bool>>>(),
                   It.IsAny<CancellationToken>()))
              .ReturnsAsync(() => null!);
            var handler = new GetProductDetailsQueryHandler(_repository.Object, _mapper, _currencyRepository.Object);

            var caughtException = await Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
             x => x.GetProductByIdAsync(
                  It.IsAny<Guid>(),
                  It.IsAny<Expression<Func<ProductSale, bool>>>(),
                  It.IsAny<CancellationToken>()))
             .ReturnsAsync(_product);
            var handler = new GetProductDetailsQueryHandler(_repository.Object, _mapper, _currencyRepository.Object);
            //Act
            var result = await handler.Handle(_query, default);
            //Assert
            Assert.NotNull(result);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<ProductDetailsDto>(result.data);
        }
    }
}
