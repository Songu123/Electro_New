using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage ="Tên không được vượt quá 50 ký tự")]
        public string Name { get; set; } = string.Empty;
        public required string Description { get; set; }

        public ICollection<Product> Products { get; set; } 
    }
}
