using Entities.Models;

namespace Contracts
{
    public interface IDownloadsRepository : IRepositoryBase<Downloads>
    {
        Task AddOrUpdate(string playerId, string mapId);
    }
}
