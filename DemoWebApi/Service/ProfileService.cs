using Contracts;
using Entities.Dto;
using Entities.Models;

namespace Service
{
    public class ProfileService
    {
        private readonly IRepositoryWrapper _repository;

        public ProfileService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<PlayerProfileDto?> GetProfile(string playerId)
        {
            Subscriptions? subscription = await _repository.Subscriptions.GetWithProfile(playerId);

            PlayerProfiles? profile;
            if (subscription == null)
                profile = await _repository.PlayerProfiles.GetWithCredentials(playerId);
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