using System.Security.Claims;
using API.Contracts;
using API.DTOS.AccountRoles;
using API.DTOS.Accounts;
using API.Models;
using API.Utilities;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEmailHandler _emailHandler;

        public AccountService(IAccountRepository accountRepository,
                         IEmployeeRepository employeeRepository,
                         IUniversityRepository universityRepository,
                         IEducationRepository educationRepository,
                         ITokenHandler tokenHandler,
                         IRoleRepository roleRepository,
                         IEmailHandler emailHandler,
                         IAccountRoleRepository accountRoleRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _tokenHandler = tokenHandler;
            _roleRepository = roleRepository;
            _emailHandler = emailHandler;
            _accountRoleRepository = accountRoleRepository;
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
                                                    OTP = account.OTP
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

        public RegisterDto? Register(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return null;
            }

            EmployeeService employeeService = new EmployeeService(_employeeRepository);
            Employee employee = new Employee
            {
                Guid = new Guid(),
                NIK = employeeService.GenerateNik(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                HiringDate = registerDto.HiringDate,
                Email = registerDto.Email,
                PhoneNumber = registerDto.Phone,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdEmployee = _employeeRepository.Create(employee);
            if (createdEmployee is null)
            {
                return null;
            }

            var univeristyEntity = _universityRepository.GetByCodeAndName(registerDto.UniversityCode,
                registerDto.UniversityName);
            if (univeristyEntity is null)
            {
                var university = new University
                {
                    Guid = new Guid(),
                    Code = registerDto.UniversityCode,
                    Name = registerDto.UniversityName,
                };
                univeristyEntity = _universityRepository.Create(university);
            }



            Education education = new Education
            {
                Guid = employee.Guid,
                Major = registerDto.Major,
                Degree = registerDto.Degree,
                GPA = registerDto.Gpa,
                UniversityGuid = univeristyEntity.Guid
            };

            var createdEducation = _educationRepository.Create(education);
            if (createdEducation is null)
            {
                return null;
            }

            Account account = new Account
            {
                Guid = employee.Guid,
                Password = Hashing.HashPassword(registerDto.Password),
            };

            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return null;
            }

            var getRoleUser = _roleRepository.GetByName("User");
            _accountRoleRepository.Create(new AccountRoleDto
            {
                AccountGuid = account.Guid,
                RoleGuid = getRoleUser.Guid
            });

            var createdAccount = _accountRepository.Create(account);
            if (createdAccount is null)
            {
                return null;
            }


            var toDto = new RegisterDto
            {
                FirstName = createdEmployee.FirstName,
                LastName = createdEmployee.LastName,
                BirthDate = createdEmployee.BirthDate,
                Gender = createdEmployee.Gender,
                HiringDate = createdEmployee.HiringDate,
                Email = createdEmployee.Email,
                Phone = createdEmployee.PhoneNumber,
                Password = createdAccount.Password,
                Major = createdEducation.Major,
                Degree = createdEducation.Degree,
                Gpa = createdEducation.GPA,
                UniversityCode = univeristyEntity.Code,
                UniversityName = univeristyEntity.Name
            };

            return toDto;
        }

        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var employee = _employeeRepository.GetEmail(changePasswordDto.email);
            if (employee is null)
                return 0; // Email not found

            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
                return 0; // Email not found

            if (account.IsUsed)
                return -1; // OTP is used

            if (account.OTP != changePasswordDto.otp)
                return -2; // OTP is incorrect

            if (account.ExpiredDate < DateTime.Now)
                return -3; // OTP is expired


            var isUpdated = _accountRepository.Update(new Account
            {
                Guid = account.Guid,
                Password = Hashing.HashPassword(changePasswordDto.newPassword),
                IsDeleted = account.IsDeleted,
                OTP = account.OTP,
                ExpiredDate = account.ExpiredDate,
                IsUsed = true,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            });

            return isUpdated ? 1    // Success
                             : -4;  // Database Error
        }

        public int ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            var employee = _employeeRepository.GetEmail(forgotPassword.Email);
            if (employee is null)
                return 0; // Email not found

            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
                return -1;
            var otp = new Random().Next(111111, 999999);
            var isUpdated = _accountRepository.Update(new Account
            {
                Guid = account.Guid,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                ExpiredDate = DateTime.Now.AddMinutes(5),
                OTP = otp,
                IsUsed = false,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            });

            if (!isUpdated)
                return -1;

            _emailHandler.SendEmail(forgotPassword.Email,
                                "Forgot Password",
                                $"Your OTP is {otp}");

            return 1;
        }

        public string LoginAccount(LoginDto login)
        {
            var employee = _employeeRepository.GetEmail(login.Email);
            if (employee is null)
                return "0";

            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
                return "0";

            if (!Hashing.ValidatePassword(login.Password, account!.Password))
                return "-1";

            try
            {
                var claims = new List<Claim>() {
                new Claim("NIK", employee.NIK),
                new Claim("FullName", $"{employee.FirstName} {employee.LastName}"),
                new Claim("EmailAddress", login.Email)
            };

                var getAccountRole = _accountRoleRepository.GetAccountRolesByAccountGuid(employee.Guid);
                var getRoleNameByAccountRole = from ar in getAccountRole
                                               join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                               select r.Name;

                claims.AddRange(getRoleNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

                var getToken = _tokenHandler.GenerateToken(claims);
                return getToken;
            }
            catch
            {
                return "-2";
            }



        }
    }
}


