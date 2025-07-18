using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace JwT_with_RefreshToken.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(88)]
        public string Token { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime ExpiresAt { get; set; }
        public User User { get; set; } = null!;
        public bool IsRevoked { get; set; } = false;

        public void CreateRefreshToken()
        {
            Token = GenerateToken();
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = DateTime.UtcNow.AddDays(15);
        }
        public string GetToken()
        {
            return Token;
        }
        string GenerateToken()
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
