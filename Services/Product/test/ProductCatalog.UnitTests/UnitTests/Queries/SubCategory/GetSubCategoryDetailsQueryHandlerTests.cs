using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryDetails;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Queries.SubCategory
{
    public class GetSubCategoryDetailsQueryHandlerTests : BaseTestHandler<ISubCategoryRepository>
    {
        private readonly GetSubCategoryDetailsQuery _query;
        private readonly Domain.SubCategory _subCategory;
        public GetSubCategoryDetailsQueryHandlerTests() : base()
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
                SubCategoryId = _subCategory.SubCategoryId
            };
        }

        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                mock => mock.GetSubCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new GetSubCategoryQueryHandler(_repository.Object, _mapper);

            var caughtException = 
                await Assert.ThrowsAsync<NotFoundExceptions>(
                    async() => await handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
               mock => mock.GetSubCategoryByIdAsync(
                   It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_subCategory);
            var handler = new GetSubCategoryQueryHandler(_repository.Object, _mapper);
            //Act
            var category = await handler.Handle(_query, default);
            //Assert
            Assert.NotNull(category);
            category.isSuccess.Should().BeTrue();
            Assert.Equal(category.data.CategoryId, _query.CategoryId);
            Assert.IsType<SubCategoryDetailsDto>(category.data);
        }
    }
}
