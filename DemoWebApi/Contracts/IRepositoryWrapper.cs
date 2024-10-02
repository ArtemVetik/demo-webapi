
namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPlayerCredentialsRepository PlayerCredentials { get; }
        IRefreshTokensRepository RefreshTokens { get; }
        IConfirmRegistrationRepository ConfirmRegistrationCodes { get; }
        IPlayerProfilesRepository PlayerProfiles { get; }
        ISubscriptionsRepository Subscriptions { get; }
        ICustomMapsRepository CustomMaps { get; }
        IDownloadsRepository Downloads { get; }
        Task SaveAsync();
    }
}
