using Moq;
using ProductCatalog.Application.Application.Commands.Manufacturer.UpdateManufacturer;
using ProductCatalog.Application.Application.Commands.Product.DeleteProduct;
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
        public UpdateProductCommandHandlerTests() 
            => _command = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Name = "test name",
            Description = "test description",
            SubCategoryId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid(),
            ManufacturerId = Guid.NewGuid(),
            Available = true,
            Price = 5000
        };

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
               .ReturnsAsync(new Domain.ProductSale()
                    .Create(_command.SubCategoryId, _command.ProductId, true)
                    .Create(
                        new Domain.Product().Create(".name", "description", Guid.NewGuid()), 
                        new Domain.Cost().Create(Guid.NewGuid(), 600, Guid.NewGuid())));
            var handler = new UpdateProductCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
