using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerList;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.UnitTests.Queries.Manufacturer
{
    public class GetManufacturerListQueryHandlerTests : BaseTestHandler<IManufacturerRepository>
    {
        private readonly GetManufacturerListQuery _query;
        public GetManufacturerListQueryHandlerTests() : base() => _query = new() { Page = 1, PageSize = 10 };

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
                mock => mock.GetAllManufacturersAsync(
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domain.Manufacturer> {
                    new Domain.Manufacturer(){
                    ManufacturerId = Guid.NewGuid(),
                    Name = "test name",
                    Description = "test description"
                },
                    new Domain.Manufacturer(){
                    ManufacturerId = Guid.NewGuid(),
                    Name = "test name1",
                    Description = "test description1"
                } });

            var handle = new GetManufacturerListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await handle.Handle(_query, default);
            //Assert
            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<List<ManufacturerListDto>>(result.data);
            result.meta.count.Should().Be(2);

        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyresult()
        {
            //Arrange 
            _repository.Setup(
                mock => mock.GetAllManufacturersAsync(
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var hadle = new GetManufacturerListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await hadle.Handle(_query, default);
            //
            Assert.Empty(result.data);
        }
    }
}
