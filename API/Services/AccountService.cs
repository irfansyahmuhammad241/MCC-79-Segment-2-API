using API.Contracts;
using API.DTOS.Accounts;
using API.Models;
using API.Utilities;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IEnumerable<GetAccountsDto>? GetAccount()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return null; // No Account  found
            }
            var toDto = accounts.Select(account =>
                                                new GetAccountsDto
                                                {
                                                    guid = account.Guid,
                                                    Password = account.Password,
                                                    IsDeleted = account.IsDeleted,
                                                    IsUsed = account.IsUsed,
                                                }).ToList();

            return toDto; // Account found
        }

        public GetAccountsDto? GetAccountByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return null; // account not found
            }

            var toDto = new GetAccountsDto
            {
                guid = account.Guid,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                IsUsed = account.IsUsed,
            };
            return toDto; // accounts found
        }

        public GetAccountsDto? CreateAccount(NewAccountsDto newAccountDto)
        {
            var account = new Account
            {
                Guid = newAccountDto.Guid,
                Password = Hashing.HashPassword(newAccountDto.Password),
                OTP = newAccountDto.OTP,
                IsUsed = newAccountDto.IsUsed,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
               
            };

            var createdAccount = _accountRepository.Create(account);
            if (createdAccount is null)
            {
                return null; // Account not created
            }

            var toDto = new GetAccountsDto
            {
                guid = createdAccount.Guid,
                Password = createdAccount.Password,
                IsDeleted = createdAccount.IsDeleted,
                IsUsed = createdAccount.IsUsed,
            };
            return toDto; // Account created
        }

        public int UpdateAccount(UpdateAccountsDto updateAccountDto)
        {
            var isExist = _accountRepository.IsExist(updateAccountDto.Guid);
            if (!isExist)
            {
                return -1; // Account not found
            }

            var getAccount = _accountRepository.GetByGuid(updateAccountDto.Guid);

            var account = new Account
            {
                Guid = updateAccountDto.Guid,
                IsUsed = updateAccountDto.IsUsed,
                Password = Hashing.HashPassword(updateAccountDto.Password),
                IsDeleted = updateAccountDto.IsDeleted,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount!.CreatedDate
            };

            var isUpdate = _accountRepository.Update(account);
            if (!isUpdate)
            {
                return 0; // Account not updated
            }

            return 1;
        }

        public int DeleteAccount(Guid guid)
        {
            var isExist = _accountRepository.IsExist(guid);
            if (!isExist)
            {
                return -1; // Account not found
            }

            var account = _accountRepository.GetByGuid(guid);
            var isDelete = _accountRepository.Delete(account!);
            if (!isDelete)
            {
                return 0; // Account not deleted
            }

            return 1;
        }
    }
}
