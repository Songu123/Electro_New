using System.ComponentModel.DataAnnotations;

namespace E_commerce.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("ConfirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
