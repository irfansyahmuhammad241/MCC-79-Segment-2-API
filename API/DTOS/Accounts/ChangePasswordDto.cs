using System.ComponentModel.DataAnnotations;
using API.Utilities;

namespace API.DTOS.Accounts
{
    public class ChangePasswordDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public int otp { get; set; }
        [PasswordPolicy]
        public string newPassword { get; set; }
        [ConfirmPassword("newPassword", ErrorMessage = "Password and Confirm Password do not match")]
        public string confirmPassword { get; set; }
    }
}
