using Azure.Core;
using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.DTO;
using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwT_with_RefreshToken.AuthService
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly AuthenticationDbContext _context;
        readonly IConfiguration _configuration;

        public AuthenticationService(AuthenticationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<CreateUserResult> CreateUserAsync(CreateUserRequest user, CancellationToken cancellationToken)
        {
            var userResult = new CreateUserResult();

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email, cancellationToken);

            if (dbUser is not null)
            {
                userResult.Success = false;
                userResult.ErrorCode = "UserExists";
                return userResult;
            }

            var roles = _configuration.GetSection("AppSettings:Roles").Get<List<string>>();

            var defaultRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roles![0], cancellationToken)
                ?? throw new Exception($"Default Role {roles![0]} dosent exist in database");

            var refreshTokenObj = new RefreshToken();
            refreshTokenObj.CreateRefreshToken();

            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Address = user.Address,
                RefreshTokens = [refreshTokenObj],
                Roles = [defaultRole]
            };

            userResult.TokenResponse.RefreshToken = refreshTokenObj.GetToken();
            userResult.TokenResponse.AccessToken = GenerateAccessToken(newUser);

            await _context.Users.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return userResult;
        }

        public async Task<TokenResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            foreach (var refreshToken in user.RefreshTokens)
            {
                refreshToken.IsRevoked = true;
            }
            var refreshTokenObj = new RefreshToken();
            refreshTokenObj.CreateRefreshToken();

            user.RefreshTokens.Add(refreshTokenObj);

            await _context.SaveChangesAsync(cancellationToken);

            return new TokenResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = refreshTokenObj.GetToken(),
            };
        }

        public async Task<bool> RevokeRefreshToken(string refresToken, CancellationToken cancellationToken)
        {
            var refreshtoken = await _context.RefreshTokens.Include(u => u.User).FirstOrDefaultAsync(rf => rf.Token == refresToken, cancellationToken: cancellationToken) ?? throw new InvalidOperationException();

            refreshtoken.IsRevoked = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u =>
                    u.RefreshTokens.Any(rt =>
                        rt.Token == refreshToken &&
                        !rt.IsRevoked &&
                        rt.ExpiresAt > DateTime.UtcNow),
                    cancellationToken);

            if (user is null)
            {
                return null;
            }

            foreach (var token in user.RefreshTokens)
            {
                token.IsRevoked = true;
            }
            var refreshTokenObj = new RefreshToken();
            refreshTokenObj.CreateRefreshToken();

            user.RefreshTokens.Add(refreshTokenObj);

            await _context.SaveChangesAsync(cancellationToken);

            return new TokenResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = refreshTokenObj.GetToken(),
            };

        }


        //------------------------------------------------------------------------------ Private Methods -------------------------------------------------------------------------------------//

        string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
           {
               new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
               new(ClaimTypes.Name, user.FirstName),
               new(ClaimTypes.Email, user.Email),
           };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:TokenKey"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return accessToken;
        }
    }
}
