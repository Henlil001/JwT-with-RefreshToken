using JwT_with_RefreshToken.DTO;

namespace JwT_with_RefreshToken.AuthService
{
    public interface IAuthenticationService
    {
        Task<TokenResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<TokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<CreateUserResult> CreateUserAsync(CreateUserRequest user, CancellationToken cancellationToken);
        Task<bool> RevokeRefreshToken(string refresToken, CancellationToken cancellationToken);
    }
}
