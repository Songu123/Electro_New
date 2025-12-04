namespace E_commerce.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Login(string password)
        {
            // Dummy implementation for illustration purposes
            return PasswordHash == password; // In real applications, use proper hashing and verification
        }
        internal void SetPassword(string v)
        {
            throw new NotImplementedException();
        }
        private bool Register(string username, string email, string password)
        {
            // Dummy implementation for illustration purposes
            return true; // In real applications, implement actual registration logic
        }

        protected void SayHello()
        {
            Console.WriteLine("Hello from User class!");
        }
    }
}
