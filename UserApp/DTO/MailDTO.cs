using System.ComponentModel.DataAnnotations;

namespace UserApp.DTO
{
    public class MailDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
