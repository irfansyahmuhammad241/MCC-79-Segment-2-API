using API.Utilities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Accounts
{
    public class NewAccountsDto
    {
        [Required]
        public Guid Guid { get; set; }
        [PasswordPolicy]
        public string Password { get; set; }
        [Required]
        public int OTP { get; set; }
        [Required]
        public bool IsUsed { get; set; }

    }
}
