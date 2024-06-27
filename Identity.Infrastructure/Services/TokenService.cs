using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Exceptions;
using Identity.Application.Common.Models.User.Get;
using Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Services
{
    public class TokenService : IUserTokenService
    {
        private readonly IAuthDbContext _dbContext;
        private readonly IMapper _mapper;
        private bool _disposed = false;
        public TokenService(IAuthDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("AuthDbContext");
            _mapper = mapper;
        }
        public async Task<UserTokenResponse> GetUserTokenAsync(string userId, string loginProvider, string tokenName)
        {
            var data =
                await _dbContext.AppUserTokens
                .Where(x => x.UserId == userId && x.LoginProvider == loginProvider && x.Name == tokenName)
                .ProjectTo<UserTokenDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new UserTokenResponse(data!);
        }

        public async Task<UserTokenResponse> GetUserTokenAsync(string token)
        {
            var data =
                await _dbContext.AppUserTokens
                .Where(x => x.Value == token)
                .ProjectTo<UserTokenDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new UserTokenResponse(data!);
        }

        public async Task RemoveUserTokenAsync(string id)
        {
            var token = 
                await _dbContext.AppUserTokens.FindAsync(new object[] { id });
            if (token is null)
                throw new NotFoundExceptions(nameof(AppUserToken), id);

            _dbContext.AppUserTokens.Remove(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetUserTokenAsync(string userId, string loginProvider, string tokenName, string? tokenValue, DateTime expDate)
        {
            var userToken =
                await _dbContext.AppUserTokens
                .FirstOrDefaultAsync(x => x.UserId == userId
                    && x.LoginProvider == loginProvider
                    && x.Name == tokenName);

            if (userToken is not null)
            {
                userToken.Value = tokenValue;
                userToken.EffectiveDate = DateTime.Now;
                userToken.ExpiryDate = expDate;
            }
            else if (userToken is null)
            {
                userToken = new AppUserToken()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    LoginProvider = loginProvider,
                    Name = tokenName,
                    Value = tokenValue,
                    EffectiveDate = DateTime.Now,
                    ExpiryDate = expDate
                };
                await _dbContext.AppUserTokens.AddAsync(userToken);
            }

            await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _dbContext.Dispose();
            }
            _disposed = true;
        }

        ~TokenService() => Dispose(false);

    }
}
