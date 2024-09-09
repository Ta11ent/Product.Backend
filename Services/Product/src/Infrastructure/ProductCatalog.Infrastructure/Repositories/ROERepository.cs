using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class ROERepository : BaseRepository, IROERepository
    {
        public ROERepository(IProductDbContext dbContext) : base(dbContext) { }

        public async Task CreateROEAsync(ROE roe, CancellationToken cancellationToken) =>
            await _dbContext.ROE.AddAsync(roe, cancellationToken);

        public void DeleteROE(ROE roe) => _dbContext.ROE.Remove(roe);

        public async Task<ROE> GetROEByIdAsync(Guid currencyId, Guid roeId, CancellationToken cancellationToken)
        {
            var data =
                await _dbContext.ROE
                    .Where(x => x.CurrecnyId == currencyId
                        && x.ROEId == roeId)
                    .FirstOrDefaultAsync(cancellationToken);
            return data!;
        }
    }
}
