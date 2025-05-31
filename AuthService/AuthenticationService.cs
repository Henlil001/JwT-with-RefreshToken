using Azure.Core;
using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.DTO;
using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;
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

            var defaultRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == _configuration.GetValue<string>("Roles:User"), cancellationToken)
                ?? throw new Exception("Default Role \"User\" dosent exist in database");

            var refreshTokenObj = new RefreshToken();
            refreshTokenObj.SetRefreshToken(GenerateRefreshToken);

            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Address = user.Address,
                RefreshToken = refreshTokenObj,
                Roles = [defaultRole]
            };

            userResult.TokenResponse.RefreshToken = newUser.RefreshToken.Token;
            userResult.TokenResponse.AccessToken = GenerateAccessToken(newUser);

            await _context.Users.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return userResult;
        }

        public async Task<TokenResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {

            var user = await _context.Users
                .Include(u => u.Roles)
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            user.RefreshToken.Token = GenerateRefreshToken();
            user.RefreshToken.CreatedAt = DateTime.UtcNow;
            user.RefreshToken.ExpiresAt = DateTime.UtcNow.AddDays(15);

            await _context.SaveChangesAsync(cancellationToken);

            return new TokenResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = user.RefreshToken.Token
            };
        }



        public async Task<TokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var existingToken = await _context.RefreshTokens
                 .Include(rt => rt.User)
                 .ThenInclude(u => u.Roles)
                 .FirstOrDefaultAsync(rt => rt.Token == refreshToken, cancellationToken);

            if (existingToken is null || existingToken.ExpiresAt < DateTime.UtcNow)
            {
                return null;
            }

            var user = existingToken.User;

            existingToken.Token = GenerateRefreshToken();
            existingToken.CreatedAt = DateTime.UtcNow;
            existingToken.ExpiresAt = DateTime.UtcNow.AddDays(15);

            await _context.SaveChangesAsync(cancellationToken);

            return new TokenResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = existingToken.Token
            };

        }


        //------------------------------------------------------------------------------ Private Methods -------------------------------------------------------------------------------------//

        string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
               new Claim(ClaimTypes.Name, user.FirstName),
               new Claim(ClaimTypes.Email, user.Email),
           };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:TokenKey")!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return accessToken;
        }
        string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            string randomBase64 = Convert.ToBase64String(randomNumber);
            string guid = Guid.NewGuid().ToString("N");
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(); 

            string refreshToken = $"{timestamp}.{guid}.{randomBase64}";
            return refreshToken;
        }
    }
}
