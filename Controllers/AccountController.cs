using E_commerce.Repositories.Interfaces;
using E_commerce.Services.Interfaces;
using E_commerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public AccountController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        //REGISTER
        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            if (_userRepository.GetAll().Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email đã tồn tại");
                return View("Register", model);
            }

            var user = new Models.User
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "User"
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return RedirectToAction("login");
        }

        //LOGIN
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginVM());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                TempData["Error"] = "Email hoặc mật khẩu không đúng";
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                return View("Login", model);
            }

            // Lưu thông tin đăng nhập vào session hoặc cookie ở 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        //LOGOUT
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        //FORGET PASSWORD
        [HttpGet("forgot-password")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepository.GetAll()
                .FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                TempData["Error"] = "Email không tồn tại trong hệ thống";
                return View(model);
            }

            // tạo token reset
            user.ResetPasswordToken = Guid.NewGuid().ToString();
            user.ResetPasswordExpire = DateTime.Now.AddMinutes(15);
            await _userRepository.SaveAsync();

            var resetLink = Url.Action(
                "ResetPassword",
                "Account",
                new { token = user.ResetPasswordToken },
                Request.Scheme
            );

            await _emailService.SendAsync(
                user.Email,
                "Đặt lại mật khẩu",
                $"Vui lòng click <a href='{resetLink}'>vào đây</a> để đổi mật khẩu"
            );

            TempData["Success"] = "Đã gửi link reset mật khẩu. Vui lòng kiểm tra email!";
            return RedirectToAction("ForgotPassword");
        }


        //RESET PASSWORD
        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            var user = _userRepository.GetAll().FirstOrDefault(u => u.ResetPasswordToken == token && u.ResetPasswordExpire > DateTime.Now);
            if (user == null)
            {
                return BadRequest("Link không hợp lệ hoặc đã hết hạn");
            }
            return View(new ResetPasswordViewModel { Token = token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ResetPassword", model);
            }
            var user = _userRepository.GetAll().FirstOrDefault(u => u.ResetPasswordToken == model.Token && u.ResetPasswordExpire > DateTime.Now);
            if (user == null)
            {
                return BadRequest("Link không hợp lệ hoặc đã hết hạn");
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.ResetPasswordToken = null;
            user.ResetPasswordExpire = null;
            await _userRepository.SaveAsync();
            return RedirectToAction("Login");
        }
    }
}
