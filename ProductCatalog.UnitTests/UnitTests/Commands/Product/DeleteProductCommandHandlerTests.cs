using Moq;
using ProductCatalog.Application.Application.Commands.Product.DeleteProduct;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Domain;
using System.Linq.Expressions;

namespace ProductCatalog.UnitTests.Commands.Product
{
    public class DeleteProductCommandHandlerTests : BaseTestHandler<IProductRepository>
    {
        private readonly DeleteProductCommand _command;
        private readonly Domain.ProductSale _product;
        public DeleteProductCommandHandlerTests()
        {
            _product = new ProductSale().Create(Guid.NewGuid(), Guid.NewGuid(), true);
            _command = new DeleteProductCommand()
            {
                CategoryId = Guid.NewGuid(),
                SubCategoryId = _product.SubCategoryId,
                ProductId = _product.ProductId
            };
        }
        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoProduct()
        {
            _repository.Setup(
                x => x.GetProductByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Expression<Func<ProductSale, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new DeleteProductCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Hendle_Should_CallDeleteOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetProductByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Expression<Func<ProductSale, bool>>>(),
                    It.IsAny<CancellationToken>()))
               .ReturnsAsync(_product);
            var handler = new DeleteProductCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.DeleteProduct(
                    It.IsAny<ProductSale>()),
                Times.Once);

        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetProductByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Expression<Func<ProductSale, bool>>>(),
                    It.IsAny<CancellationToken>()))
               .ReturnsAsync(_product);
            var handler = new DeleteProductCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
