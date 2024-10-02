using Contracts;
using Entities.Dto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class ProfileService
    {
        private IRepositoryWrapper _repository;

        public ProfileService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<PlayerProfileDto> GetProfile(string playerId)
        {
            PlayerProfiles profile = null;
            Subscriptions subscription = await _repository.Subscriptions.FindByCondition(data => data.player_id == playerId).FirstOrDefaultAsync();

            if (subscription == null)
                profile = await _repository.PlayerProfiles.FindByCondition(data => data.player_id == playerId).FirstOrDefaultAsync();
            else
                profile = subscription.PlayerProfile;

            if (profile == null)
                return null;

            return new PlayerProfileDto()
            {
                id = playerId,
                email = profile.PlayerCredential.email,
                name = profile.name,
                gender = profile.gender,
                created_at = profile.created_at,
                subscribed = subscription != null,
                subscription_expires_at = subscription?.expires_at,
            };
        }
    }
}