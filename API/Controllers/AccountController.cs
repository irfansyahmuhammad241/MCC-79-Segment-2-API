using System.Net;
using API.DTOS.AccountRoles;
using API.DTOS.Accounts;
using API.Services;
using API.Utilities;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]
    [Authorize(Roles = $"{nameof(RoleLevel.Admin)}")]

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
        [AllowAnonymous]
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
        [AllowAnonymous]
        //[Authorize(Roles = $"{nameof(RoleLevel.User)}")]
        public IActionResult ForgetPassword(ForgotPasswordDto forgetPasswordDto)
        {
            var isUpdated = _service.ForgotPassword(forgetPasswordDto);
            if (isUpdated == 0)
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found"
                });

            if (isUpdated is -1)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieving data from the database"
                });

            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Otp has been sent to your email"
            });

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult LoginRequest(LoginDto login)
        {
            var loginResult = _service.LoginAccount(login);
            if (loginResult == "0")
                return NotFound(new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Account not found"
                });

            if (loginResult == "-1")
                return BadRequest(new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Password is incorrect"
                });

            if (loginResult == "-2")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieving when creating token"
                });
            }

            return Ok(new ResponseHandler<string>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = loginResult
            });
        }

        [HttpPut("ChangePassword")]
        [AllowAnonymous]
        public IActionResult UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            var isUpdated = _service.ChangePassword(changePasswordDto);
            if (isUpdated == 0)
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found"
                });

            if (isUpdated == -1)
            {
                return BadRequest(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Otp is already used"
                });
            }

            if (isUpdated == -2)
            {
                return BadRequest(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Otp is incorrect"
                });
            }

            if (isUpdated == -3)
            {
                return BadRequest(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Otp is expired"
                });
            }

            if (isUpdated is -4)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieving data from the database"
                });

            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Password has been changed successfully"
            });


        }

    }

}
