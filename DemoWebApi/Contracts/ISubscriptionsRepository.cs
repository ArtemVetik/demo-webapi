using Entities.Models;

namespace Contracts
{
    public interface ISubscriptionsRepository : IRepositoryBase<Subscriptions>
    {
        Task<Subscriptions?> GetWithProfile(string playerId);
    }
}
