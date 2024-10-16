using AutoMapper;
using Moq;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.UnitTests
{
    public class BaseTestHandler<T> where T : class
    {
        protected readonly Mock<T> _repository;
        protected readonly IMapper _mapper;
        public BaseTestHandler()
        {
            _repository = new();
            _mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(typeof(IProductDbContext).Assembly));
            }).CreateMapper();
        }
    }
}
