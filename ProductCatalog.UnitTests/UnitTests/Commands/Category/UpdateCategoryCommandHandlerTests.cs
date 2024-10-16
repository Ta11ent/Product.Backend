using Moq;
using ProductCatalog.Application.Application.Commands.Category.UpdateCategory;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Category
{
    public class UpdateCategoryCommandHandlerTests : BaseTestHandler<ICategoryRepository>
    {
        private readonly UpdateCategoryCommand _command;
        public UpdateCategoryCommandHandlerTests() : base() =>
            _command = new() { CategoryId = Guid.NewGuid(), Name = "TestName", Description = "TestDescription" };

        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new UpdateCategoryCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
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
            var handler = new UpdateCategoryCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
