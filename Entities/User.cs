using System.ComponentModel.DataAnnotations;

namespace JwT_with_RefreshToken.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(250)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(150)]
        public string Address { get; set; }
        [Required]
        public RefreshToken RefreshToken { get; set; }
        public ICollection<Role> Roles { get; set; } = [];


    }
}
