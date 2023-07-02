using System.Net;
using API.DTOS.AccountRoles;
using API.DTOS.Accounts;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;
        private readonly EmployeeService _employeeService;

        public AccountController(AccountService accountService, EmployeeService employee)
        {
            _service = accountService;
            _employeeService = employee;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetAccount();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<GetAccountsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetAccountsDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = entities
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var account = _service.GetAccountByGuid(guid);
            if (account is null)
            {
                return NotFound(new ResponseHandler<GetAccountsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<GetAccountsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = account
            });
        }

        [HttpPost]
        public IActionResult Create(NewAccountsDto newAccountDto)
        {
            var createAccount = _service.CreateAccount(newAccountDto);
            if (createAccount is null)
            {
                return BadRequest(new ResponseHandler<GetAccountsDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data not created"
                });
            }

            return Ok(new ResponseHandler<GetAccountsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully created",
                Data = createAccount
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateAccountsDto updateAccountDto)
        {
            var update = _service.UpdateAccount(updateAccountDto);
            if (update is -1)
            {
                return NotFound(new ResponseHandler<UpdateAccountRolesDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (update is 0)
            {
                return BadRequest(new ResponseHandler<UpdateAccountRolesDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateAccountRolesDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var delete = _service.DeleteAccount(guid);

            if (delete is -1)
            {
                return NotFound(new ResponseHandler<GetAccountsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (delete is 0)
            {
                return BadRequest(new ResponseHandler<GetAccountsDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check connection to database"
                });
            }

            return Ok(new ResponseHandler<GetAccountsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully deleted"
            });
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register(RegisterDto register)
        {
            var createdRegister = _service.Register(register);
            if (createdRegister == null)
            {
                return BadRequest(new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Register failed"
                });
            }

            return Ok(new ResponseHandler<RegisterDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully register",
                Data = createdRegister
            });
        }

        [HttpPost("forget-password")]
        public IActionResult ForgetPassword(ForgotPasswordDto forgetPasswordDto)
        {
            var getAccount = _employeeService.GetByEmail(forgetPasswordDto.Email);
            if (getAccount is null)
            {
                return NotFound(new ResponseHandler<ForgotPasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "No Account found with the given email"
                });
            }

            // Generate OTP
            Random random = new Random();
            HashSet<int> uniqueDigits = new HashSet<int>();

            while (uniqueDigits.Count < 6)
            {
                int digit = random.Next(0, 9);
                uniqueDigits.Add(digit);
            }

            int generatedOtp = uniqueDigits.Aggregate(0, (acc, digit) => acc * 10 + digit);

            // Get Account By Guid
            var relatedAccount = _service.GetAccountByGuid(getAccount.Guid)!;

            // Update Otp, Expired Time, isUsed in Account
            var updateAccountDto = new UpdateAccountsDto
            {
                Guid = relatedAccount.guid,
                Password = relatedAccount.Password,
                IsDeleted = (bool)relatedAccount.IsDeleted,
                Otp = generatedOtp,
                IsUsed = false,
                ExpiredTime = DateTime.Now.AddMinutes(5)
            };

            var updateResult = _service.UpdateAccount(updateAccountDto);
            if (updateResult == 0)
            {
                return NotFound(new ResponseHandler<ForgotPasswordDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to Setting OTP in Related Account"
                });
            }

            // Success to Create OTP and Update the Account Model
            return Ok(new ResponseHandler<IEnumerable<OtpResponseDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Account found",
                Data = new List<OtpResponseDto> { new OtpResponseDto {
                    Guid = getAccount.Guid,
                    Email = getAccount.Email,
                    Otp = generatedOtp
                } }
            });

        }

        [HttpPost("Login")]
        public IActionResult LoginRequest(LoginDto login)
        {
            var entities = _service.Login(login);
            if (entities is "-1")
            {
                return BadRequest(new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Password didn't match"
                });

            }

            if (entities is "0")
            {
                return NotFound(new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email not found"
                });
            }

            if (entities is "-2")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Database Error"
                });
            }

            return Ok(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success To Login",
            });
        }

        [HttpPut("ChangePassword")]
        public IActionResult UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            var update = _service.ChangePassword(changePasswordDto);
            if (update is -1)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found"
                });
            }

            if (update is 0)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Otp doesn't match"
                });
            }

            if (update is 1)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Otp doesn't match"
                });
            }

            if (update is 2)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Otp Already Expired"
                });
            }

            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfuly Updated"
            });


        }

    }

}
