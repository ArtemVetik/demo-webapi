using Entities.Models;

namespace Contracts
{
    public interface IConfirmRegistrationRepository : IRepositoryBase<ConfirmRegistrationCodes>
    {
        Task<bool> Has(string email);
        Task<ConfirmRegistrationCodes> Get(string email, string code);
    }
}
