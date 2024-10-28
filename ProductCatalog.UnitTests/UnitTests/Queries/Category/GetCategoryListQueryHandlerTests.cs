using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryList;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.UnitTests.Queries.Category
{
    public class GetCategoryListQueryHandlerTests : BaseTestHandler<ICategoryRepository>
    {
        private readonly GetCategoryListQuery _query;
        private readonly Domain.Category _category;
        public GetCategoryListQueryHandlerTests() : base()
        {
            _category = new()
            {
                CategoryId = Guid.NewGuid(),
                Name = "test name",
                Description = "test description"
            };
            _query = new() {
                Page = 1,
                PageSize = 10 
            };
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
                mock => mock.GetAllCategoriesAsync(
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domain.Category> { _category });

            var handle = new GetCategoryListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await handle.Handle(_query, default);
            //Assert
            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<List<CategoryListDto>>(result.data);
            result.meta.count.Should().Be(1);

        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyresult()
        {
            //Arrange 
            _repository.Setup(
                mock => mock.GetAllCategoriesAsync(
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var hadle = new GetCategoryListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await hadle.Handle(_query, default);
            //
            Assert.Empty(result.data);
        }
    }
}
