using System.ComponentModel.DataAnnotations;

namespace JwT_with_RefreshToken.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public ICollection<Role> Roles { get; set; } = [];

        public User(int userId, string email, string password, string firstName, string lastName, string address, ICollection<RefreshToken> refreshTokens)
        {
            UserId = userId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            RefreshTokens = refreshTokens;
        }
    }
}
