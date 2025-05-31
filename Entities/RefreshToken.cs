using System.ComponentModel.DataAnnotations;

namespace JwT_with_RefreshToken.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(15);

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
