using E_commerce.Models;
using E_commerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryController(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAll());
        }
    }
}
