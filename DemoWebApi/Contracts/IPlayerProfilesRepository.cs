using Entities.Models;

namespace Contracts
{
    public interface IPlayerProfilesRepository : IRepositoryBase<PlayerProfiles>
    {
        Task<PlayerProfiles> GetWithCredentials(string playerId);
    }
}
