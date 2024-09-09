using AutoMapper;
using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Queries.Category
{
    public class GetCategoryDetailsQueryHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapper;
        private readonly GetCategoryDetailsQuery _query;
        public GetCategoryDetailsQueryHandlerTests()
        {
            _categoryRepositoryMock = new();
            _mapper = new();
            _query = new() { CategoryId = Guid.NewGuid() };
        }

        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _categoryRepositoryMock.Setup(
                            x => x.GetCategoryByIdAsync(
                                It.IsAny<Guid>(),
                                It.IsAny<CancellationToken>()))
                            .ReturnsAsync(() => null!);

            var handler = new GetCategoryDetailsQueryHandler(_categoryRepositoryMock.Object, _mapper.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
        }
        
        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _categoryRepositoryMock.Setup(
               mock => mock.GetCategoryByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Category()
               {
                   CategoryId = _query.CategoryId,
                   Name = "test name",
                   Description = "test description"
               });
            _mapper.Setup
                (mock => mock.Map<CategoryDetailsDto>(
                    It.IsAny<Domain.Category>()))
                .Returns(new CategoryDetailsDto()
                {
                    CategoryId = _query.CategoryId,
                    Name = "test name",
                    Description = "test description"
                });
            var handler = new GetCategoryDetailsQueryHandler(_categoryRepositoryMock.Object, _mapper.Object);
            //Act
            var category = await handler.Handle(_query, default);
            //Assert
            Assert.NotNull(category);
            category.isSuccess.Should().BeTrue();   
            Assert.Equal(category.data.CategoryId, _query.CategoryId);
        }
    }
}
