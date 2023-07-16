using API.DTOS.Accounts;
using API.Utilities;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<GetAccountsDto, string>
    {
        public Task<ResponseHandler<string>> Login(LoginDto entity);
        public Task<ResponseHandler<string>> Register(RegisterDto entity);
    }
}
