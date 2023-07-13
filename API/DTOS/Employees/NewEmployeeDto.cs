using System.ComponentModel.DataAnnotations;
using API.Utilities.Enums;

namespace API.DTOS.Employees
{
    public class NewEmployeeDto
    {

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime HiringDate { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
