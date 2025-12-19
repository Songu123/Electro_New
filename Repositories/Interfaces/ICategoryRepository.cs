using E_commerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public interface ICategoryRepository: IGenericRepository<Category>
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<List<SelectListItem>> GetSelectListAsync();
}
