using Moq;
using FluentAssertions;
using ProductCatalog.Application.Application.Commands.Category.CreateCategory;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.UnitTests.Commands.Category
{
    public class CreateCategoryCommandHandlerTests : BaseTestHandler<ICategoryRepository>
    {
        private readonly CreateCategoryCommand _command =
            new CreateCategoryCommand() { Name = "TestName", Description = "TestDescription" };
        public CreateCategoryCommandHandlerTests() : base() { }

        [Fact]
        public async Task Handle_Should_CallAddOnRepository()
        {
            //Arrange
            var handler = new CreateCategoryCommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.CreateCategoryAsync(
                    It.IsAny<Domain.Category>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            var handler = new CreateCategoryCommandHandler(_repository.Object);
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
            var handler = new CreateCategoryCommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
