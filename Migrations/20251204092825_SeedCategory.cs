using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_commerce.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Các dòng smartphone mới nhất", "Điện thoại" },
                    { 2, "Máy tính xách tay cho học tập và làm việc", "Laptop" },
                    { 3, "Máy tính bảng nhiều kích thước màn hình", "Tablet" },
                    { 4, "Cáp sạc, tai nghe, bao da, ốp lưng", "Phụ kiện" },
                    { 5, "Màn hình máy tính độ phân giải cao", "Màn hình" },
                    { 6, "Loa bluetooth, tai nghe chụp tai", "Âm thanh" },
                    { 7, "Router, bộ phát wifi, switch", "Thiết bị mạng" },
                    { 8, "Smartwatch theo dõi sức khỏe", "Đồng hồ thông minh" },
                    { 9, "Máy tính để bàn gaming và văn phòng", "PC – Máy bàn" },
                    { 10, "Máy ảnh kỹ thuật số và phụ kiện", "Máy ảnh" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
