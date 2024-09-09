using AutoMapper;
using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryList;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.UnitTests.Queries.Category
{
    public class GetCategoryListQueryHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapper;
        private readonly GetCategoryListQuery _query;
        public GetCategoryListQueryHandlerTests()
        {
            _categoryRepositoryMock = new();
            _mapper = new();
            _query = new() { Page = 1, PageSize = 10 };
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _categoryRepositoryMock.Setup(
               x => x.GetAllCategoriesAsync(
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new List<Domain.Category> {new Domain.Category()
                        {
                            CategoryId = Guid.NewGuid(),
                            Name = "test name",
                            Description = "test description"
                        }});
            _mapper.Setup
                (mock => mock.Map<List<CategoryDetailsDto>>(
                    It.IsAny<Domain.Category>()))
                        .Returns(new List<CategoryDetailsDto> { new CategoryDetailsDto()
                        {
                            CategoryId = Guid.NewGuid(),
                            Name = "test name",
                            Description = "test description"
                        }});
            var handle = new GetCategoryListQueryHandler(_categoryRepositoryMock.Object, _mapper.Object);
            //Act
            var result = await handle.Handle(_query, default);
            //Assert
            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
        }
    }
}
