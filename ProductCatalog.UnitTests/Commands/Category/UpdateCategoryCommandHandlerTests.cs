using Moq;
using ProductCatalog.Application.Application.Commands.Category.UpdateCategory;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Category
{
    public class UpdateCategoryCommandHandlerTests
    {
        private readonly Guid _categoryId = Guid.NewGuid();
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly UpdateCategoryCommand _command;
        public UpdateCategoryCommandHandlerTests() {
            _categoryRepositoryMock = new();
            _command = new() { CategoryId = _categoryId, Name = "TestName", Description = "TestDescription" };
        }

        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _categoryRepositoryMock.Setup(
                x => x.GetCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new UpdateCategoryCommandHandler(_categoryRepositoryMock.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _categoryRepositoryMock.Setup(
               x => x.GetCategoryByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Category()
               {
                   CategoryId = _categoryId,
                   Name = "test name",
                   Description = "test description"
               });
            var handler = new UpdateCategoryCommandHandler(_categoryRepositoryMock.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _categoryRepositoryMock.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
