using Microsoft.AspNetCore.Mvc;

namespace BookLibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
