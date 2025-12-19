using E_commerce.Models;
using E_commerce.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;


        public ProductsController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepo.GetWithCategoryAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productRepo.GetWithCategoryByIdAsync(id.Value);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepo.GetAll().ToListAsync();

            ViewBag.Categories = new SelectList(
                categories,
                "Id",
                "Name"
            );

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {

            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("imageFile", "Vui lòng chọn hình ảnh");
            }


            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($"❌ {entry.Key}: {error.ErrorMessage}");
                }
            }



            if (product.CategoryId == 0)
            {
                ModelState.AddModelError("CategoryId", "Please select a category");
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(stream);

                    product.ImageUrl = fileName;

                }

                Console.WriteLine("👉 BEFORE ADD");
                await _productRepo.AddAsync(product);
                await _productRepo.SaveAsync();
                Console.WriteLine("✅ AFTER SAVE");

                ;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(
                _categoryRepo.GetAll(),
                "Id",
                "Name",
                product.CategoryId
            );

            return View(product);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productRepo.GetByIdAsync(id.Value);
            if (product == null) return NotFound();

            ViewBag.CategoryId = await _categoryRepo.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product p)
        {
            if (id != p.Id) return NotFound();

            if (!ModelState.IsValid) return View(p);

            //Lấy dữ liệu Database
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();

            //Cập nhật Description
            product.Description = p.Description;

            //Xử lý logic thay đổi ảnh
            if(p.ImageUrl != null && p.ImageUrl.Length > 0)
            {
                string newImagePath = await SaveIma
            }

            if (ModelState.IsValid)
            {
                await _productRepo.UpdateAsync(p);
                await _productRepo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productRepo.GetByIdAsync(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepo.DeleteAsync(id);
            await _productRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}