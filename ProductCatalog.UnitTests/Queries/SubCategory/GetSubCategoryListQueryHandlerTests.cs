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
        public GetSubCategoryListQueryHandlerTests() : base() => _query = new()
        {
            CategoryId = Guid.NewGuid(),
            Page = 1,
            PageSize = 10
        };

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
                mock => mock.GetAllSubCategoriesAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domain.SubCategory> {
                   new Domain.SubCategory(){
                   SubCategoryId = Guid.NewGuid(),
                   Name = "test name",
                   Description = "test description",
                   CategoryId = _query.CategoryId
                },
                   new Domain.SubCategory(){
                   SubCategoryId = Guid.NewGuid(),
                   Name = "test name1",
                   Description = "test description1",
                   CategoryId = _query.CategoryId
                }});

            var handle = new GetSubCategoryListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await handle.Handle(_query, default);
            //Assert
            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<List<SubCategoryListDto>>(result.data);
            result.meta.count.Should().Be(2);
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
