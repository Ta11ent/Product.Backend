using Moq;
using ProductCatalog.Application.Application.Commands.Category.DeleteCategory;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Category
{
    public class DeleteCategoryCommandHandlerTests : BaseTestHandler<ICategoryRepository>
    {
        private readonly DeleteCategoryCommand _command;
        public DeleteCategoryCommandHandlerTests() : base() => _command = new() { CategoryId = Guid.NewGuid() };

        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new DeleteCategoryCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Hendle_Should_CallDeleteOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetCategoryByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Category()
               {
                   CategoryId = _command.CategoryId,
                   Name = "test name",
                   Description = "test description"
               });
            var handler = new DeleteCategoryCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.DeleteCategory(
                    It.IsAny<Domain.Category>()),
                Times.Once);

        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetCategoryByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Category()
               {
                   CategoryId = _command.CategoryId,
                   Name = "test name",
                   Description = "test description"
               });
            var handler = new DeleteCategoryCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
