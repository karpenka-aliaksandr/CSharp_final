using System.ComponentModel.DataAnnotations;
using UserApp.Model;

namespace UserApp.DTO
{
    public class MailRoleDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public RoleType Role { get; set; }
        //public RoleType UserRole { get; set; } = RoleType.MailRoleDTO;
    }
}
