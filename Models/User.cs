using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Customer";

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool Status { get; set; } = true;

        //RESET PASSWORD
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpire { get; set; }

    }
}
