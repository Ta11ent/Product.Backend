using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Queries.Category
{
    public class GetCategoryDetailsQueryHandlerTests : BaseTestHandler<ICategoryRepository>
    {
        private readonly GetCategoryDetailsQuery _query;
        private readonly Domain.Category _category;
        public GetCategoryDetailsQueryHandlerTests() : base()
        {
            _category = new()
            {
                CategoryId = Guid.NewGuid(),
                Name = "test name",
                Description = "test description"
            };
            _query = new() {
                CategoryId = _category.CategoryId
            };
        }

        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                mock => mock.GetCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new GetCategoryDetailsQueryHandler(_repository.Object, _mapper);

            var caughtException = await Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
               mock => mock.GetCategoryByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_category);
            var handler = new GetCategoryDetailsQueryHandler(_repository.Object, _mapper);
            //Act
            var category = await handler.Handle(_query, default);
            //Assert
            Assert.NotNull(category);
            category.isSuccess.Should().BeTrue();
            Assert.Equal(category.data.CategoryId, _query.CategoryId);
            Assert.IsType<CategoryDetailsDto>(category.data);
        }
    }
}
