using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SubscriptionsRepository : RepositoryBase<Subscriptions>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<Subscriptions?> GetWithProfile(string playerId)
        {
            if (RepositoryContext.Subscriptions == null)
                return null;

            return await RepositoryContext.Subscriptions
                .Include(p => p.PlayerProfile)
                .ThenInclude(c => c.PlayerCredential)
                .FirstOrDefaultAsync(data => data.player_id == playerId);
        }
    }
}
