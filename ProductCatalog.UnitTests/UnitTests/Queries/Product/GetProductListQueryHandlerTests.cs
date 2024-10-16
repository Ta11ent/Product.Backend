using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Product.GetProductList;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;
using System.Linq.Expressions;

namespace ProductCatalog.UnitTests.UnitTests.Queries.Product
{
    public class GetProductListQueryHandlerTests : BaseTestHandler<IProductRepository>
    {
        private readonly GetProductListQuery _query;
        private readonly Mock<ICurrencyRepository> _currencyRepository;
        public GetProductListQueryHandlerTests() {
            _query = new()
            {
                CategoryId = Guid.NewGuid(),
                SubCategoryId = Guid.NewGuid(),
                Available = true,
                ProductIds = [Guid.NewGuid(), Guid.NewGuid()],
                Page = 1,
                PageSize = 10
            };
            _currencyRepository = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            _repository.Setup(
                mock => mock.GetAllProductsAsync(
                    It.IsAny<Expression<Func<ProductSale, bool>>>(),
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProductSale>() {
                    new ProductSale().Create(Guid.NewGuid(), Guid.NewGuid()),
                    new ProductSale().Create(Guid.NewGuid(), Guid.NewGuid())
                });

            var handle = new GetProductListQueryHandler(_repository.Object, _currencyRepository.Object, _mapper);

            var result = await handle.Handle(_query, default);

            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<List<ProductListDto>>(result.data);
            result.meta.count.Should().Be(2);

        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyresult()
        {
            _repository.Setup(
               mock => mock.GetAllProductsAsync(
                   It.IsAny<Expression<Func<ProductSale, bool>>>(),
                   It.IsAny<IPagination>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(() => null!);

            var hadle = new GetProductListQueryHandler(_repository.Object, _currencyRepository.Object, _mapper);

            var result = await hadle.Handle(_query, default);

            Assert.Empty(result.data);
        }
    };
}