using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
