using Contracts;
using Entities.Dto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class AccountService
    {
        private IRepositoryWrapper _repository;
        private JwtTokenService _tokenService;

        public AccountService(IRepositoryWrapper repository, JwtTokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task SendConfirmCode(string email)
        {
            var data = new ConfirmRegistrationCodes()
            {
                email = email,
                code = ConfirmCodeFactory.CreateNew(),
                expire_at = DateTime.UtcNow.AddMinutes(15)
            };

            if (await _repository.ConfirmRegistrationCodes.Has(email))
                _repository.ConfirmRegistrationCodes.Update(data);
            else
                await _repository.ConfirmRegistrationCodes.CreateAsync(data);

            await _repository.SaveAsync();

            // Send code to email
            // ...
        }

        public async Task<bool> ConsumeConfirmationCode(string email, string code)
        {
            var confirmData = await _repository.ConfirmRegistrationCodes.Get(email, code);

            if (confirmData == null || DateTime.UtcNow > confirmData.expire_at)
                return false;

            _repository.ConfirmRegistrationCodes.Delete(confirmData);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<bool> Registration(RegistrationDto registrationData)
        {
            if (await _repository.PlayerCredentials.Has(registrationData.email))
                return false;

            var playerId = Guid.NewGuid().ToString();

            await _repository.PlayerCredentials.CreateAsync(new PlayerCredentials()
            {
                player_id = playerId,
                email = registrationData.email,
                password = registrationData.password,
            });

            await _repository.PlayerProfiles.CreateAsync(new PlayerProfiles
            {
                player_id = playerId,
                name = registrationData.name,
                gender = registrationData.gender,
                created_at = DateTime.UtcNow,
            });

            await _repository.SaveAsync();
            return true;
        }

        public async Task<LoginTokensDto> Login(LoginCredentialsDto loginData)
        {
            var credentials = await _repository.PlayerCredentials.Get(loginData.email, loginData.password);

            if (credentials == null)
                return null;

            var refreshToken = _tokenService.CreateRefreshToken();

            var oldToken = await _repository.RefreshTokens.Get(credentials.player_id);

            if (oldToken != null)
            {
                oldToken.refresh_token = refreshToken.Token;
                oldToken.expires_at = refreshToken.ExpiresAt;
                _repository.RefreshTokens.Update(oldToken);
            }
            else
            {
                await _repository.RefreshTokens.CreateAsync(new RefreshTokens()
                {
                    player_id = credentials.player_id,
                    refresh_token = refreshToken.Token,
                    expires_at = refreshToken.ExpiresAt,
                });
            }

            await _repository.SaveAsync();

            return new LoginTokensDto()
            {
                access = _tokenService.CreateAccessToken(credentials.player_id),
                refresh = refreshToken.Token,
            };
        }

        public async Task<LoginTokensDto> Refresh(string refreshToken)
        {
            var oldToken = await _repository.RefreshTokens.FindByCondition(data => data.refresh_token == refreshToken).FirstOrDefaultAsync();

            if (oldToken == null)
                return null;

            if (DateTime.UtcNow > oldToken.expires_at)
                return null;

            var newRefreshToken = _tokenService.CreateRefreshToken();

            oldToken.refresh_token = newRefreshToken.Token;
            oldToken.expires_at = newRefreshToken.ExpiresAt;
            _repository.RefreshTokens.Update(oldToken);

            await _repository.SaveAsync();

            return new LoginTokensDto()
            {
                access = _tokenService.CreateAccessToken(oldToken.player_id),
                refresh = newRefreshToken.Token,
            };
        }
    }
}