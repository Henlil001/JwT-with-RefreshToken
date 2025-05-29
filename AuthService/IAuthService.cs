using JwT_with_RefreshToken.DTO;

namespace JwT_with_RefreshToken.AuthService
{
    public interface IAuthService
    {
        Task<TokenResponse?> LoginAsync(TokenRequest request, CancellationToken cancellationToken);
        Task<TokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<CreateUserResult> CreateUserAsync(CreateUserRequest user, CancellationToken cancellationToken);
    }
}
