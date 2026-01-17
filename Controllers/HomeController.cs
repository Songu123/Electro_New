using System.Diagnostics;
using E_commerce.Models;
using E_commerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Store()
        {
            var products = _productRepository.GetAll();

            return View(products);
        }

        public IActionResult About()
        {
            return Content("?ây là trang gi?i thi?u v? S?n");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
