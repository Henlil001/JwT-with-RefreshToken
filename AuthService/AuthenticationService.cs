using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.DTO;
using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwT_with_RefreshToken.AuthService
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly AuthenticationDbContext _context;

        public AuthenticationService(AuthenticationDbContext context)
        {
            _context = context;
        }

        public Task<CreateUserResult> CreateUserAsync(CreateUserRequest user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(u => u.Email == request.Email).Include(u => u.Roles).FirstOrDefaultAsync();

        }

        public Task<TokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        string GenerateAccesToken(User user)
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

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "",
                audience: "",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return accessToken;
        }
        string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            string refreshToken = Convert.ToBase64String(randomNumber);
            return refreshToken;
        }
    }
}
