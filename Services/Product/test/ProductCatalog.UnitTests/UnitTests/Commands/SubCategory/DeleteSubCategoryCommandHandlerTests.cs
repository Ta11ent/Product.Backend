using Moq;
using ProductCatalog.Application.Application.Commands.SubCategory.DeleteSubCategory;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.SubCategory
{
    public class DeleteSubCategoryCommandHandlerTests : BaseTestHandler<ISubCategoryRepository>
    {
        private readonly DeleteSubCategoryCommand _command;
        private readonly Domain.SubCategory _subcategory;
        public DeleteSubCategoryCommandHandlerTests() : base()
        {
            _subcategory = new()
            {
                CategoryId = Guid.NewGuid(),
                SubCategoryId = Guid.NewGuid(),
                Name = "test name",
                Description = "test description",
               
            };
            _command = new()
            {
                SubCategoryId = _subcategory.SubCategoryId,
                CategoryId = _subcategory.CategoryId
            };
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetSubCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new DeleteSubCategoryCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Hendle_Should_CallDeleteOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetSubCategoryByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_subcategory);
            var handler = new DeleteSubCategoryCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.DeleteSubCategory(
                    It.IsAny<Domain.SubCategory>()),
                Times.Once);

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
               .ReturnsAsync(_subcategory);
            var handler = new DeleteSubCategoryCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
