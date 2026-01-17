using E_commerce.ViewModels;

namespace E_commerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginVM model, HttpContext context);
        Task RegisterAsync(RegisterVM model);

        Task LogoutAsync(HttpContext context);
    }
}
