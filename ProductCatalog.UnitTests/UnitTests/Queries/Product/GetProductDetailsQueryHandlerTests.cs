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
        public GetProductDetailsQueryHandlerTests() {
            _query = new GetProductDetailsQuery()
            {
                CategoryId = Guid.NewGuid(),
                SubCategoryId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
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
              .ReturnsAsync(new Domain.ProductSale().Create(Guid.NewGuid(), Guid.NewGuid(), true));
            var handler = new GetProductDetailsQueryHandler(_repository.Object, _mapper, _currencyRepository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
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
             .ReturnsAsync(
                new Domain.ProductSale()
                    .Create(Guid.NewGuid(), Guid.NewGuid(), true));
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
