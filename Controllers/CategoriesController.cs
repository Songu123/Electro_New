using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerce.Models;

namespace E_commerce.Controllers
{
    [Route("admin/category")]
    public class CategoriesController : Controller
    {
        private readonly IGenericRepository<Category> _repo;

        public CategoriesController(IGenericRepository<Category> repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll().ToListAsync());
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _repo.GetByIdAsync(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(category);
                await _repo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _repo.GetByIdAsync(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repo.UpdateAsync(category);
                await _repo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _repo.GetByIdAsync(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost("delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
