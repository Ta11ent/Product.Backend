using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Commands.SubCategory.CreateSubCategory;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.UnitTests.Commands.SubCategory
{
    public class CreateSubCategoryCommandHandlerTests : BaseTestHandler<ISubCategoryRepository>
    {
        private readonly CreateSubCategoryCommand _command;
        public CreateSubCategoryCommandHandlerTests() : base() => _command = new()
        {
            CategoryId = Guid.NewGuid(),
            Name = "test name",
            Description = "test description"
        };
        [Fact]
        public async Task Handle_Should_CallAddOnRepository()
        {
            //Arrange
            var handler = new CreateSubCategoryQueryHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.CreateSubCategoryAsync(
                    It.IsAny<Domain.SubCategory>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            var handler = new CreateSubCategoryQueryHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            result.Should().NotBeEmpty();
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task Handle_Should_CallSaveChanges()
        {
            //Arrange
            var handler = new CreateSubCategoryQueryHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
