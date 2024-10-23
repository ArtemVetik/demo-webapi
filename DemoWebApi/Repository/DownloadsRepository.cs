using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DownloadsRepository : RepositoryBase<Downloads>, IDownloadsRepository
    {
        public DownloadsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task AddOrUpdate(string playerId, string mapId)
        {
            if (RepositoryContext.Downloads == null)
                return;

            var hasDownload = await RepositoryContext.Downloads.AnyAsync(data => data.player_id == playerId && data.map_id == mapId);

            if (hasDownload)
                return;

            await RepositoryContext.Downloads.AddAsync(new Downloads()
            {
                map_id = mapId,
                player_id = playerId,
            });

            await RepositoryContext.SaveChangesAsync();
        }
    }
}
