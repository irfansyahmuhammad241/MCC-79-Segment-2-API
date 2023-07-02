using System.Net;
using API.DTOS.Employees;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetEmployee();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<GetEmployeesDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetEmployeesDto>>
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
            var employee = _service.GetEmployee(guid);
            if (employee is null)
            {
                return NotFound(new ResponseHandler<GetEmployeesDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<GetEmployeesDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = employee
            });
        }

        [HttpPost]
        public IActionResult Create(NewEmployeeDto newEmployeeDto)
        {
            var createEmployee = _service.CreateEmployee(newEmployeeDto);
            if (createEmployee is null)
            {
                return BadRequest(new ResponseHandler<GetEmployeesDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data not created"
                });
            }

            return Ok(new ResponseHandler<GetEmployeesDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully created",
                Data = createEmployee
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateEmployeeDto updateEmployeeDto)
        {
            var update = _service.UpdateEmployee(updateEmployeeDto);
            if (update is -1)
            {
                return NotFound(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (update is 0)
            {
                return BadRequest(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var delete = _service.DeleteEmployee(guid);

            if (delete is -1)
            {
                return NotFound(new ResponseHandler<GetEmployeesDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (delete is 0)
            {
                return BadRequest(new ResponseHandler<GetEmployeesDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check connection to database"
                });
            }

            return Ok(new ResponseHandler<GetEmployeesDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully deleted"
            });
        }

        [HttpGet("get-all-master")]
        public IActionResult GetMaster()
        {
            var entities = _service.GetMaster();
            if (entities is null)
            {
                return NotFound(new ResponseHandler<EmployeeEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<EmployeeEducationDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = entities
            });
        }

        [HttpGet("get-all-master")]
        public IActionResult GetMasterByGuid(Guid guid)
        {
            var entities = _service.GetByMasterGuid(guid);
            if (entities is null)
            {
                return NotFound(new ResponseHandler<EmployeeEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<EmployeeEducationDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = entities
            });
        }

    }
}
