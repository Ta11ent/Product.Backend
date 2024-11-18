using Moq;
using ProductCatalog.Application.Application.Commands.SubCategory.UpdateSubCategory;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.SubCategory
{
    public class UpdateSubCategoryCommandHandlerTests : BaseTestHandler<ISubCategoryRepository>
    {
        private readonly UpdateSubCategoryCommand _command;
        private readonly Domain.SubCategory _subCategory;
        public UpdateSubCategoryCommandHandlerTests() : base()
        {
            _subCategory = new()
            {
                SubCategoryId = Guid.NewGuid(),
                Name = "test name",
                Description = "test description",
                CategoryId = Guid.NewGuid()
            };
            _command = new()
            {
                CategoryId = _subCategory.CategoryId,
                SubCategoryId = _subCategory.SubCategoryId,
                Name = "test name",
                Description = "test description"
            };
        }

        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetSubCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new UpdateSubCategoryCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetSubCategoryByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_subCategory);
            var handler = new UpdateSubCategoryCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

    }
}
