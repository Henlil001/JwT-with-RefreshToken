using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public void SetRefreshToken(Func<string> func)
        {
            Token = func();
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = DateTime.UtcNow.AddDays(15);
        }
    }
}
