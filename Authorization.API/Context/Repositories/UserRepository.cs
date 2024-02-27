using Exodus.Cotacao.Authorization.Context.Repositories.Interfaces;
using Exodus.Cotacao.Authorization.Models.Users;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Exodus.Cotacao.Authorization.Context.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _appDbContext = new AppDbContext(_configuration);
        }

        public void Register(User user)
        {
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
        }

        public User? Check(string CPF)
        {
            return _appDbContext.Users.FirstOrDefault(u => u.CPF == CPF);
        }

        public User Login(string CPForEmail, string Password)
        {
            return _appDbContext.Users.FirstOrDefault(u => u.CPF == CPForEmail && u.Password == Password || u.Email == CPForEmail && u.Password == Password);
        }

        public User? Delete(string Password, string ConfirmPassword)
        {
            return _appDbContext.Users.Find(Password, ConfirmPassword);
        }

    }
}
