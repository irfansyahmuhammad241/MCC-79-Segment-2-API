using System.ComponentModel.DataAnnotations;
using API.Utilities;

namespace API.DTOS.Accounts
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPolicy]
        public string Password { get; set; }


    }
}
