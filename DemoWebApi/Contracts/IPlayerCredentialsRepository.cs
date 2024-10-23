using Entities.Models;

namespace Contracts
{
    public interface IPlayerCredentialsRepository : IRepositoryBase<PlayerCredentials>
    {
        Task<bool?> Has(string email);
        Task<PlayerCredentials?> Get(string email, string password);
    }
}
