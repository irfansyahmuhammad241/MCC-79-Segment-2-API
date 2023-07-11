using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeDataController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}