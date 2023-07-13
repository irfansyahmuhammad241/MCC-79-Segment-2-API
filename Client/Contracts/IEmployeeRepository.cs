using API.DTOS.Employees;

namespace Client.Contracts
{
    public interface IEmployeeRepository : IRepository<GetEmployeeDto, Guid>
    {

    }
}
