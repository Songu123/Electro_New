using E_commerce.Data;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll() =>
        await _context.Set<T>().ToListAsync();
    
    public async Task<T> GetById(int id) =>
        await _context.Set<T>().FindAsync(id);

    public async Task Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var obj = await GetById(id);
        _context.Set<T>().Remove(obj);
        await _context.SaveChangesAsync();
    }
}
