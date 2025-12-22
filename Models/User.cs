using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(8)]
        public string Password { get; set; } = string.Empty;


        public string ImageIcon { get; set; } = string.Empty;
        
    }
}
