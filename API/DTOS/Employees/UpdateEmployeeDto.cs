using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Employees
{
    public class UpdateEmployeeDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        [Required]
        public DateTime HiringDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
