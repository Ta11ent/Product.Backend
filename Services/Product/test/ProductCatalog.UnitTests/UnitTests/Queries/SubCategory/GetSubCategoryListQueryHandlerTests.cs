using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryList;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.UnitTests.Queries.SubCategory
{
    public class GetSubCategoryListQueryHandlerTests : BaseTestHandler<ISubCategoryRepository>
    {
        private readonly GetSubCategoryListQuery _query;
        private readonly Domain.SubCategory _subCategory;
        public GetSubCategoryListQueryHandlerTests() : base()
        {
            _subCategory = new()
            {
                CategoryId = Guid.NewGuid(),
                SubCategoryId = Guid.NewGuid(),
                Name = "test name",
                Description = "test description",
            };
            _query = new()
            {
                CategoryId = _subCategory.CategoryId,
                Page = 1,
                PageSize = 10
            };
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
                mock => mock.GetAllSubCategoriesAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domain.SubCategory> { _subCategory});

            var handle = new GetSubCategoryListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await handle.Handle(_query, default);
            //Assert
            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<List<SubCategoryListDto>>(result.data);
            result.meta.count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyresult()
        {
            //Arrange 
            _repository.Setup(
                mock => mock.GetAllSubCategoriesAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var hadle = new GetSubCategoryListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await hadle.Handle(_query, default);
            //
            Assert.Empty(result.data);
        }
    }
}
