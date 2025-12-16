using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace E_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }                 

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage ="Tên không được vượt quá 50 ký tự")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Description { get; set; } = string.Empty;

        // Giữ kiểu cũ để tránh lỗi Hot Reload
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
