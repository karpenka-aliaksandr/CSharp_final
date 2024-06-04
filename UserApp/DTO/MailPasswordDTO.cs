using System.ComponentModel.DataAnnotations;

namespace UserApp.DTO
{
    public class MailPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        //public RoleType UserRole { get; set; } = RoleType.MailRoleDTO;
    }
}
