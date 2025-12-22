using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class ProductEditVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        // Dùng để hiển thị ảnh hiện tại trên UI
        public string? ExistingImageUrl { get; set; }


        // Dùng để nhận file mới nếu người dùng muốn thay đổi
        public IFormFile? ImageUpload { get; set; }
    }
}
