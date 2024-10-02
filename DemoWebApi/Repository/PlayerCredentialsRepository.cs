using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PlayerCredentialsRepository : RepositoryBase<PlayerCredentials>, IPlayerCredentialsRepository
    {
        public PlayerCredentialsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<bool> Has(string email)
        {
            return await RepositoryContext.PlayerCredentials.FirstOrDefaultAsync(data => data.email == email) != null;
        }

        public async Task<PlayerCredentials> Get(string email, string password)
        {
            return await RepositoryContext.PlayerCredentials.FirstOrDefaultAsync(data => data.email == email && data.password == password);
        }
    }
}
