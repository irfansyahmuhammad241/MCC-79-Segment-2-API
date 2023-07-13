using API.DTOS.Employees;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var ListEmployee = new List<GetEmployeeDto>();

            if (result.Data != null)
            {
                ListEmployee = result.Data.ToList();
            }
            return View(ListEmployee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GetEmployeeDto newEmploye)
        {

            var result = await repository.Post(newEmploye);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await repository.Delete(guid);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data Berhasil Dihapus";
            }
            else
            {
                TempData["Error"] = "Gagal Menghapus Data";
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var result = await repository.Get(guid);

            var employeeDto = new GetEmployeeDto
            {
                Guid = result.Data.Guid,
                NIK = result.Data.NIK,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                BirthDate = result.Data.BirthDate,
                Gender = result.Data.Gender,
                HiringDate = result.Data.HiringDate,
                Email = result.Data.Email,
                PhoneNumber = result.Data.PhoneNumber,
            };

            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GetEmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var result = await repository.Put(employee.Guid, employee);

            if (result.Status == "200")
            {
                TempData["Success"] = "Successfully updated";
            }

            else
            {
                TempData["Error"] = "Failed to Updated";
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
