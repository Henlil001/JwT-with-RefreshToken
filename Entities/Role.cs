using System.ComponentModel.DataAnnotations;

namespace JwT_with_RefreshToken.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
