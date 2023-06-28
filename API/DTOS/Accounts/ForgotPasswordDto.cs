using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Accounts
{
    public class ForgotPasswordDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
