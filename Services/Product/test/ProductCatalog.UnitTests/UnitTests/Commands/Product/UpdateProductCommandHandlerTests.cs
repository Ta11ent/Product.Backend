using Moq;
using ProductCatalog.Application.Application.Commands.Product.UpdateProduct;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Domain;
using System.Linq.Expressions;

namespace ProductCatalog.UnitTests.Commands.Product
{
    public class UpdateProductCommandHandlerTests : BaseTestHandler<IProductRepository>
    {
        private readonly UpdateProductCommand _command;
        private readonly ProductSale _product;
        public UpdateProductCommandHandlerTests()
        {
            _product = new ProductSale()
                .Create(Guid.NewGuid(), Guid.NewGuid(), true)
                .Create(new Domain.Product().Create(".name", "description", Guid.NewGuid()),
                        new Domain.Cost().Create(Guid.NewGuid(), 600, Guid.NewGuid()));

            _command = new UpdateProductCommand()
            {
                ProductId = _product.ProductId,
                Name = "test name",
                Description = "test description",
                SubCategoryId = _product.SubCategoryId,
                CategoryId = Guid.NewGuid(),
                ManufacturerId = Guid.NewGuid(),
                Available = true,
                Price = 5000
            };
        }

        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoProduct()
        {
            //Arrange
            _repository.Setup(
               x => x.GetProductByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Expression<Func<ProductSale, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);
            var handler = new UpdateProductCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
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
            var handler = new UpdateProductCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
