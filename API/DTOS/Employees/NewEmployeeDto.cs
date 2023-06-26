using API.Utilities.Enums;

namespace API.DTOS.Employees
{
    public class NewEmployeeDto
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiringDate { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
