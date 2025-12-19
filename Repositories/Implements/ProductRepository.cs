using E_commerce.Data;
using E_commerce.Models;
using E_commerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositories.Implements
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public IQueryable<Product> GetAllWithCategory()
        {
            return _context.Products.Include(p => p.Category);
        }

        public async Task<List<Product>> GetWithCategoryAsync()
        {
            return await _dbSet.Include(p => p.Category).ToListAsync();
        }
        public async Task<Product?> GetWithCategoryByIdAsync(int id)
        {
            return await _dbSet.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
