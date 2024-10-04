using Entities.Models;

namespace Contracts
{
    public interface IConfirmRegistrationRepository : IRepositoryBase<ConfirmRegistrationCodes>
    {
        Task<ConfirmRegistrationCodes> Get(string email, string code);
    }
}
