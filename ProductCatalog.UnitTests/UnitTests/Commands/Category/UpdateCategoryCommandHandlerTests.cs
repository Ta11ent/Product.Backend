using Moq;
using ProductCatalog.Application.Application.Commands.Category.UpdateCategory;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Category
{
    public class UpdateCategoryCommandHandlerTests : BaseTestHandler<ICategoryRepository>
    {
        private readonly UpdateCategoryCommand _command;
        private readonly Domain.Category _category;
        public UpdateCategoryCommandHandlerTests() : base()
        {
            _category = new()
            {
                CategoryId = Guid.NewGuid(),
                Name = "test name",
                Description = "test description"
            };
            _command = new()
            {
                CategoryId = _category.CategoryId,
                Name = "TestName",
                Description = "TestDescription"
            };

        }

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
               .ReturnsAsync(_category);
            var handler = new UpdateCategoryCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
