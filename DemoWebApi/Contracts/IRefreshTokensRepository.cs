using Entities.Models;

namespace Contracts
{
    public interface IRefreshTokensRepository : IRepositoryBase<RefreshTokens>
    {
        Task<RefreshTokens> Get(string playerId);
    }
}
