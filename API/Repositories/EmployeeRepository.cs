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
    }
}
