using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ConfirmRegistrationCodesRepository : RepositoryBase<ConfirmRegistrationCodes>, IConfirmRegistrationRepository
    {
        public ConfirmRegistrationCodesRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {

        }

        public async Task<bool> Has(string email)
        {
            return await RepositoryContext.ConfirmRegistrationCodes.FirstOrDefaultAsync(data => data.email == email) != null;
        }

        public async Task<ConfirmRegistrationCodes> Get(string email, string code)
        {
            return await RepositoryContext.ConfirmRegistrationCodes.FirstOrDefaultAsync(data => data.email == email && data.code == code);
        }
    }
}
