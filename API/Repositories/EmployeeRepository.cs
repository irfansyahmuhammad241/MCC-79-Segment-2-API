using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context)
        {
        }
        public IEnumerable<Employee> GetByEmail(string email)
        {
            return _context.Set<Employee>().Where(u => u.Email == email);
        }

        public Employee? GetEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(u => u.Email == email);
        }

        public Employee? GetByEmailAndPhoneNumber(string data)
        {
            return _context.Set<Employee>().FirstOrDefault(u => u.PhoneNumber == data || u.Email == data);
        }

        public Employee? CheckEmail(string email)
        {
            return _context.Set<Employee>().FirstOrDefault(u => u.Email == email);
        }
    }
}
