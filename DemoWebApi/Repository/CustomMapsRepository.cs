using Entities;
using Entities.Models;
using Contracts;

namespace Repository
{
    public class CustomMapsRepository : RepositoryBase<CustomMaps>, ICustomMapsRepository
    {
        public CustomMapsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
