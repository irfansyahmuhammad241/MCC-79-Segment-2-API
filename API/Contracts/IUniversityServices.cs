using API.DTOS.Universities;

namespace API.Contracts
{
    public interface IUniversityServices
    {
        IEnumerable<GetUniversityDto> GetUniversityDto();
    } 
}
