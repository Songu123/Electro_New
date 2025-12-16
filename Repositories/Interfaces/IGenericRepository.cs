using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
    IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
}
