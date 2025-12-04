using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
    new Category
    {
        Id = 1,
        Name = "Điện thoại",
        Description = "Các dòng smartphone mới nhất",
    },
    new Category
    {
        Id = 2,
        Name = "Laptop",
        Description = "Máy tính xách tay cho học tập và làm việc",
    },
    new Category
    {
        Id = 3,
        Name = "Tablet",
        Description = "Máy tính bảng nhiều kích thước màn hình",
    },
    new Category
    {
        Id = 4,
        Name = "Phụ kiện",
        Description = "Cáp sạc, tai nghe, bao da, ốp lưng",
    },
    new Category
    {
        Id = 5,
        Name = "Màn hình",
        Description = "Màn hình máy tính độ phân giải cao",
    },
    new Category
    {
        Id = 6,
        Name = "Âm thanh",
        Description = "Loa bluetooth, tai nghe chụp tai",
    },
    new Category
    {
        Id = 7,
        Name = "Thiết bị mạng",
        Description = "Router, bộ phát wifi, switch",
    },
    new Category
    {
        Id = 8,
        Name = "Đồng hồ thông minh",
        Description = "Smartwatch theo dõi sức khỏe",
    },
    new Category
    {
        Id = 9,
        Name = "PC – Máy bàn",
        Description = "Máy tính để bàn gaming và văn phòng",
    },
    new Category
    {
        Id = 10,
        Name = "Máy ảnh",
        Description = "Máy ảnh kỹ thuật số và phụ kiện",
    }
);

        }

    }

}