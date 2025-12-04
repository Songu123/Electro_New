using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class ProductController : Controller
    {
        static List<Product> Products = new()
        {
            new Product {Id = 1, Name = "Son", Price = 3.5},
            new Product {Id = 2, Name = "Nam", Price = 3.8}
        };
        public IActionResult Index()
        {
            return View(Products);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Product p)
        {
            p.Id = Products.Max(x => x.Id) + 1;
            Products.Add(p);
            return RedirectToAction("Index");
        }


        public IActionResult Detail()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Laptop Dell XPS 13",
                Price = 999.99
            };

            return View(product);
        }
    }
}
