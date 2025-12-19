using E_commerce.Models;

namespace E_commerce.Repositories.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {

        Task<List<Product>> GetWithCategoryAsync();
        Task<Product?> GetWithCategoryByIdAsync(int id);
    }
}
