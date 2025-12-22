using E_commerce.Data;
using E_commerce.Models;
using E_commerce.Repositories.Interfaces;

namespace E_commerce.Repositories.Implements
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { 
        }
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public  bool UserExists(int id)
        {
               return _context.Users.Any(e => e.Id == id);
        }
    }
}
