using Exodus.Cotacao.Authorization.Context.Repositories.Interfaces;
using Exodus.Cotacao.Authorization.Models.Users;
using Exodus.Cotacao.Authorization.Services.Interfaces;

namespace Exodus.Cotacao.Authorization.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) => _userRepository = userRepository;
        public void Register(User user) => _userRepository.Register(user);
        public User? Check(string CPF) => _userRepository.Check(CPF);
        public User? Login(string CPForEmail, string Password) => _userRepository.Login(CPForEmail, Password);
        public User? Delete(string Password, string ConfirmPassword) => _userRepository.Delete(Password, ConfirmPassword);

    }
}
