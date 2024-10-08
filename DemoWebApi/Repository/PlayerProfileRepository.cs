using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PlayerProfileRepository : RepositoryBase<PlayerProfiles>, IPlayerProfilesRepository
    {
        public PlayerProfileRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<PlayerProfiles> GetWithCredentials(string playerId)
        {
            return await RepositoryContext.PlayerProfiles
                .Include(c => c.PlayerCredential)
                .FirstOrDefaultAsync(data => data.player_id == playerId);
        }
    }
}
