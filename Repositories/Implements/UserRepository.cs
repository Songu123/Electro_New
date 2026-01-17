using E_commerce.Data;
using E_commerce.Models;
using E_commerce.Repositories.Interfaces;

namespace E_commerce.Repositories.Implements
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        //private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context) { 
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
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
