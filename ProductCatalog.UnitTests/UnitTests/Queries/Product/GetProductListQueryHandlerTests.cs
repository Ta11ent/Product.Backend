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
        private readonly Domain.ProductSale _product;
        public GetProductListQueryHandlerTests() {
            _product = new ProductSale().Create(Guid.NewGuid(), Guid.NewGuid());
            _query = new()
            {
                CategoryId = Guid.NewGuid(),
                SubCategoryId = _product.SubCategoryId,
                Available = true,
                ProductIds = [_product.ProductId],
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
                .ReturnsAsync(new List<ProductSale>() { _product });

            var handle = new GetProductListQueryHandler(_repository.Object, _currencyRepository.Object, _mapper);

            var result = await handle.Handle(_query, default);

            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<List<ProductListDto>>(result.data);
            result.meta.count.Should().Be(1);

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