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
        private readonly IFileService _fileService;


        public ProductsController(IProductRepository productRepo, ICategoryRepository categoryRepo, IFileService fileService)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _fileService = fileService;
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

            //Map Product -> ProductEditVm
            var vm = new ProductEditVm
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                ExistingImageUrl = product.ImageUrl
            };

            ViewBag.CategoryId = await _categoryRepo.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name

                })
                .ToListAsync();

            return View(vm);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditVm vm)
        {
            if (id != vm.Id) return NotFound();

            //Load Category dropdown
            async Task LoadCategory()
            {
                ViewBag.CategoryId = await _categoryRepo.GetAll().
                    Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync();
            }

            if (!ModelState.IsValid)
            {
                await LoadCategory();
                return View(vm);
            }

            //Lấy dữ liệu Database
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();

            //Update Text Fields
            product.Name = vm.Name;
            product.Price = vm.Price;
            product.CategoryId = vm.CategoryId;

            //Cập nhật Description
            product.Description = vm.Description;

            //Xử lý logic thay đổi ảnh
            if (vm.ImageUpload != null && vm.ImageUpload.Length > 0)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    //Delete old image
                    _fileService.DeleteFile(product.ImageUrl);
                }

                //Save new image url
                product.ImageUrl = await _fileService.SaveFileAsync(vm.ImageUpload, "products");
            }

            //Save to database
            try
            {
                await _productRepo.UpdateAsync(product);
                await _productRepo.SaveAsync();
                TempData["Success"] = "Product updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                vm.ExistingImageUrl = product.ImageUrl;
                await LoadCategory();
                ModelState.AddModelError(string.Empty, "An error occurred while updating the product: " + ex.Message);
                return View(vm);
            }

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