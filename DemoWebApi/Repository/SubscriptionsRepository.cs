using Entities;
using Entities.Models;
using Contracts;

namespace Repository
{
    public class SubscriptionsRepository : RepositoryBase<Subscriptions>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
