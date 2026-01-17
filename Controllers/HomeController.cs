using E_commerce.Models;
using E_commerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Store(List<int> categories, decimal? minPrice, decimal? maxPrice, string? sortBy, int page = 1, int pageSize = 9)
        {
            // Lấy tất cả sản phẩm với Category
            var products = _productRepository.GetAll()
       .Include(p => p.Category)
    .AsQueryable();

            // Lọc theo categories
            if (categories != null && categories.Any())
            {
                products = products.Where(p => categories.Contains(p.CategoryId));
            }

            // Lọc theo giá
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= (double)minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= (double)maxPrice.Value);
            }

            // Sắp xếp
            products = sortBy switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "name_asc" => products.OrderBy(p => p.Name),
                "name_desc" => products.OrderByDescending(p => p.Name),
                _ => products.OrderBy(p => p.Id) // Mặc định
            };

            // Đếm tổng số sản phẩm (trước khi phân trang)
            var totalProducts = await products.CountAsync();

            // Phân trang
            var paginatedProducts = await products
                .Skip((page - 1) * pageSize)
    .Take(pageSize)
          .ToListAsync();

            // Lấy danh sách categories với số lượng sản phẩm
            ViewBag.Categories = await _categoryRepository.GetAll()
                .Include(c => c.Products)
     .ToListAsync();

            // Dữ liệu phân trang
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            ViewBag.TotalProducts = totalProducts;
            ViewBag.PageSize = pageSize;

            // Giữ lại các filter hiện tại
            ViewBag.SelectedCategories = categories ?? new List<int>();
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SortBy = sortBy;

            return View(paginatedProducts);
        }

        public IActionResult About()
        {
            return Content("Đây là trang giới thiệu về Sản phẩm");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
