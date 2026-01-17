using System.ComponentModel.DataAnnotations;

namespace E_commerce.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
