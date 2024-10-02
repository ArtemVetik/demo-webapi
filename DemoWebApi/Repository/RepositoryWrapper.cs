using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IPlayerProfilesRepository _playerProfiles;
        private IConfirmRegistrationRepository _confirmRegistrationCodes;
        private IPlayerCredentialsRepository _playerCredentials;
        private IRefreshTokensRepository _refreshTokens;
        private ISubscriptionsRepository _subscriptions;
        private ICustomMapsRepository _customMaps;
        private IDownloadsRepository _downloads;

        public IPlayerProfilesRepository PlayerProfiles
            => _playerProfiles ?? (_playerProfiles = new PlayerProfileRepository(_repoContext));

        public IConfirmRegistrationRepository ConfirmRegistrationCodes
            => _confirmRegistrationCodes ?? (_confirmRegistrationCodes = new ConfirmRegistrationCodesRepository(_repoContext));

        public IPlayerCredentialsRepository PlayerCredentials
            => _playerCredentials ?? (_playerCredentials = new PlayerCredentialsRepository(_repoContext));

        public IRefreshTokensRepository RefreshTokens
           => _refreshTokens ?? (_refreshTokens = new RefreshTokensRepository(_repoContext));

        public ISubscriptionsRepository Subscriptions
            => _subscriptions ?? (_subscriptions = new SubscriptionsRepository(_repoContext));

        public ICustomMapsRepository CustomMaps
           => _customMaps ?? (_customMaps = new CustomMapsRepository(_repoContext));

        public IDownloadsRepository Downloads
          => _downloads ?? (_downloads = new DownloadsRepository(_repoContext));

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
