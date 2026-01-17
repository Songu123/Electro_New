using E_commerce.Services.Interfaces;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
                return View(model);

            var success = await _authService.LoginAsync(model, HttpContext);

            if (success)
            {
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
