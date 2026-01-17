using E_commerce.Models;
using System.Linq.Expressions;

namespace E_commerce.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //Task<List<User>> GetWithUserAsync();

        //Task<User?> GetWithCategoryByIdAsync(int id);

         bool UserExists(int id);

        User GetById(int id);
        User GetByEmail(string email);

        User GetByUsername(string username);

        void Add(User user);
    }
}
