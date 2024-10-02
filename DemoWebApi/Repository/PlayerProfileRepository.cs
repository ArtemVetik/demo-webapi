using Entities;
using Entities.Models;
using Contracts;

namespace Repository
{
    public class PlayerProfileRepository : RepositoryBase<PlayerProfiles>, IPlayerProfilesRepository
    {
        public PlayerProfileRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
