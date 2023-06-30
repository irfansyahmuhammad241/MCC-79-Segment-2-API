using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        IEnumerable<Employee> GetByEmail(string email);

        Employee GetEmail(string email);
    }
}
