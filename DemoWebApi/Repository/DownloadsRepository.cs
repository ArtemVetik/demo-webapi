using Entities;
using Entities.Models;
using Contracts;

namespace Repository
{
    public class DownloadsRepository : RepositoryBase<Downloads>, IDownloadsRepository
    {
        public DownloadsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
