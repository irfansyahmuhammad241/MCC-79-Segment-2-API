using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Utilities;
using API.Utilities.Enums;

namespace API.DTOS.Accounts
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(0, 1)]
        public GenderEnum Gender { get; set; }

        [Required]
        public DateTime HiringDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Major { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        [Range(0, 4)]
        public Double Gpa { get; set; }

        [Required]
        public string UniversityCode { get; set; }

        [Required]
        public string UniversityName { get; set; }

        [Required]
        [PasswordPolicy]
        public string Password { get; set; }

        [Required]
        [NotMapped]
        [Compare(nameof(Password), ErrorMessage = "Password Do not Match")]
        public string ConfirmPassword { get; set; }

    }
}
