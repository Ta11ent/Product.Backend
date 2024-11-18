using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Commands.Product.CreateProduct;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.UnitTests.Commands.Product
{
    public class CreateProductCommandHandlerTests : BaseTestHandler<IProductRepository>
    {
        private readonly CreateProductCommand _command;
        private readonly Mock<ISubCategoryRepository> _subCategoryRepository = new();
        public CreateProductCommandHandlerTests() =>
            _command = new CreateProductCommand()
            {
                Name = "test name",
                Description = "test description",
                Price = 6000,
                SubCategoryId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                ManufacturerId = Guid.NewGuid(),
                CurrencyId = Guid.NewGuid()
            };

        [Fact]
        public async Task Handle_Should_CallAddOnRepository()
        {
            //Arrange
            _subCategoryRepository.Setup(
                x => x.GetSubCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.SubCategory()
                {
                    SubCategoryId = _command.SubCategoryId,
                    Name = "test name",
                    Description = "test description",
                    CategoryId = _command.CategoryId
                });
            var handler = new CreateProductCommandHandler(_repository.Object, _subCategoryRepository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.CreateProductAsync(
                    It.IsAny<Domain.Product>(),
                    It.IsAny<Domain.ProductSale>(),
                    It.IsAny<Domain.Cost>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _subCategoryRepository.Setup(
                x => x.GetSubCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.SubCategory()
                {
                    SubCategoryId = _command.SubCategoryId,
                    Name = "test name",
                    Description = "test description",
                    CategoryId = _command.CategoryId
                });
            var handler = new CreateProductCommandHandler(_repository.Object, _subCategoryRepository.Object);
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
            _subCategoryRepository.Setup(
                x => x.GetSubCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.SubCategory()
                {
                    SubCategoryId = _command.SubCategoryId,
                    Name = "test name",
                    Description = "test description",
                    CategoryId = _command.CategoryId
                });
            var handler = new CreateProductCommandHandler(_repository.Object, _subCategoryRepository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
