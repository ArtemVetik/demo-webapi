using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RefreshTokensRepository : RepositoryBase<RefreshTokens>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<RefreshTokens?> Get(string playerId)
        {
            if (RepositoryContext.RefreshTokens == null)
                return null;

            return await RepositoryContext.RefreshTokens.FirstOrDefaultAsync(data => data.player_id == playerId);
        }
    }
}
