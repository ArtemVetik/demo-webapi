using Contracts;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class MapService
    {
        private IRepositoryWrapper _repository;

        public MapService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<MapInfoDto[]> GetAllMaps()
        {
            var result = await (from map in _repository.CustomMaps.FindAll()
                                join download in _repository.Downloads.FindAll()
                                on map.map_id equals download.map_id into downloadsGroup
                                from download in downloadsGroup.DefaultIfEmpty()
                                group download by map into grouped
                                select new MapInfoDto
                                {
                                    map_id = grouped.Key.map_id,
                                    name = grouped.Key.name,
                                    description = grouped.Key.description,
                                    player_id = grouped.Key.player_id,
                                    downloads = grouped.Count(d => d != null),
                                }).ToArrayAsync();

            return result;
        }

        public async Task<string> DownloadMap(string playerId, string mapId)
        {
            var map = await _repository.CustomMaps.FindByCondition(data => data.map_id == mapId).FirstOrDefaultAsync();

            if (map == null)
                return null;

            await _repository.Downloads.AddOrUpdate(playerId, mapId);

            return map.download_url;
        }

        public async Task UploadMap(string playerId, UploadMapDto mapData)
        {
            await _repository.CustomMaps.CreateAsync(new Entities.Models.CustomMaps()
            {
                map_id = Guid.NewGuid().ToString(),
                player_id = playerId,
                name = mapData.name,
                description = mapData.description,
                download_url = mapData.download_url,
            });

            await _repository.SaveAsync();
        }
    }
}