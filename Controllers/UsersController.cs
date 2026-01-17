using E_commerce.Models;
using E_commerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    [Route("admin/user")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        public UsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAll().ToListAsync());
        }


        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userRepository.GetByIdAsync(id.Value);

            if (user == null) return NotFound();

            return View(user);
        }


        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id,  User user)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userRepository.GetByIdAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost("delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
