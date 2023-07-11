using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class PokemonDataController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}